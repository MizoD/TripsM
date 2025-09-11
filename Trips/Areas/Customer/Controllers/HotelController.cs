using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Request.HotelRequest;
using System.Security.Claims;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HotelController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HotelController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int pageNumber = 1,
            int pageSize = 6,
            string sortBy = "price",
            string sortOrder = "asc",
            string? country = null,
            string? city = null,
            int? numberOfGuests = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 6;

            var hotels = await unitOfWork.HotelRepository.GetAsync(
                includes: h => h.Include(h => h.Country)
                                .Include(h => h.Trip)
                                .Include(h => h.HotelImages)); // Include images

            // Filters
            if (!string.IsNullOrWhiteSpace(country))
                hotels = hotels.Where(h => h.Country.Name.Contains(country, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(city))
                hotels = hotels.Where(h => h.City.Contains(city, StringComparison.OrdinalIgnoreCase));

            if (numberOfGuests.HasValue && numberOfGuests.Value > 0)
                hotels = hotels.Where(h => h.AvailableRooms >= numberOfGuests.Value);

            // Sorting
            hotels = sortBy.ToLower() switch
            {
                "price" => sortOrder.ToLower() == "desc"
                    ? hotels.OrderByDescending(h => h.PricePerNight)
                    : hotels.OrderBy(h => h.PricePerNight),
                "traffic" => sortOrder.ToLower() == "desc"
                    ? hotels.OrderByDescending(h => h.Traffic)
                    : hotels.OrderBy(h => h.Traffic),
                _ => hotels.OrderBy(h => h.PricePerNight)
            };

            // Pagination
            var totalCount = hotels.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var data = hotels.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var model = new PaginatedHotelResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = data
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await unitOfWork.HotelRepository.GetOneAsync(
                h => h.Id == id,
                includes: h => h.Include(h => h.Country)
                                .Include(h => h.Trip)
                                .Include(h => h.Bookings)
                                .Include(h => h.HotelImages)
            );

            if (hotel == null)
                return NotFound();

            var relatedHotels = await unitOfWork.HotelRepository.GetAsync(
                h => h.Id != id && h.CountryId == hotel.CountryId,
                includes: h => h.Include(h => h.Country).Include(h => h.HotelImages));

            if (!relatedHotels.Any())
            {
                relatedHotels = await unitOfWork.HotelRepository.GetAsync(
                    h => h.Id != id && h.City == hotel.City,
                    includes: h => h.Include(h => h.Country).Include(h => h.HotelImages));
            }

            var vm = new HotelDetailsViewVM
            {
                Hotel = hotel,
                RelatedHotels = relatedHotels.Take(4).ToList()
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int hotelId, DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (checkInDate >= checkOutDate || checkInDate < DateTime.Today)
            {
                TempData["Error"] = "Invalid check-in/check-out dates.";
                return RedirectToAction("Details", new { id = hotelId });
            }

            var hotel = await unitOfWork.HotelRepository.GetOneAsync(t => t.Id == hotelId);
            if (hotel == null)
                return NotFound();

            if (hotel.AvailableRooms < numberOfGuests)
            {
                TempData["Error"] = "Not enough available rooms.";
                return RedirectToAction("Details", new { id = hotelId });
            }

            var existingCartItem = await unitOfWork.HotelCartRepository.GetOneAsync(
                c => c.UserId == user.Id && c.HotelId == hotelId
            );

            if (existingCartItem != null)
                existingCartItem.NumberOfPassengers += numberOfGuests;
            else
            {
                var cartItem = new HotelCart
                {
                    HotelId = hotel.Id,
                    UserId = user.Id,
                    NumberOfPassengers = numberOfGuests,
                    AddedAt = DateTime.UtcNow
                };
                await unitOfWork.HotelCartRepository.CreateAsync(cartItem);
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = "Room added to cart successfully.";
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int hotelId)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            var hotel = await unitOfWork.HotelRepository.GetOneAsync(t => t.Id == hotelId);
            if (hotel == null)
                return NotFound();

            var existingWishlistItem = await unitOfWork.HotelWishlistRepository.GetOneAsync(
                w => w.UserId == user.Id && w.HotelId == hotelId
            );

            if (existingWishlistItem != null)
            {
                TempData["Error"] = "This hotel is already in your wishlist.";
                return RedirectToAction("Details", new { id = hotelId });
            }

            var wishlistItem = new HotelWishlist
            {
                HotelId = hotelId,
                UserId = user.Id,
                AddedAt = DateTime.UtcNow
            };

            await unitOfWork.HotelWishlistRepository.CreateAsync(wishlistItem);
            await unitOfWork.CommitAsync();

            TempData["Success"] = "Hotel added to wishlist successfully.";
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> Search(HotelSearchRequest request, int pageNumber = 1, int pageSize = 6, string sortBy = "price", string sortOrder = "asc")
        {

            var hotels = await unitOfWork.HotelRepository.GetAsync(
                h => h.AvailableRooms >= request.NumberOfGuests / 2 &&
                     h.Country.Name.ToLower().Contains(request.Country.ToLower()),
                includes: h => h.Include(h => h.Country)
                                .Include(h => h.Trip)
                                .Include(h => h.Bookings)
            );

            // Sorting
            hotels = sortBy.ToLower() switch
            {
                "price" => sortOrder.ToLower() == "desc" ? hotels.OrderByDescending(h => h.PricePerNight) : hotels.OrderBy(h => h.PricePerNight),
                _ => hotels.OrderBy(h => h.PricePerNight)
            };

            // Pagination
            var totalCount = hotels.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedData = hotels.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var model = new HotelSerachResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = paginatedData
            };

            return View("Index", model);
        }
    }
}
