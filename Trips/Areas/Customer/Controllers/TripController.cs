using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Request.TripRequest;
using System.Security.Claims;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TripController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public TripController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 6)
        {
            var allTrips = await unitOfWork.TripRepository.GetAllAvailableTripsAsync();
            int totalCount = allTrips.Count();

            var pagedTrips = allTrips
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripResponse
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description!,
                    Price = t.Price,
                    ImageUrl = t.ImageUrl,
                    CountryName = t.Country!.Name,
                    AverageRating = t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0,
                    ReviewCount = t.Reviews.Count,
                    IsAvailable = t.IsAvailable,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    DurationDays = t.DurationDays,
                    TripType = t.TripType,
                    TotalSeats = t.TotalSeats,
                    AvailableSeats = t.AvailableSeats,
                })
                .ToList();

            var response = new PaginatedTripResponse
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Trips = pagedTrips
            };

            return View(response); 
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var trip = await unitOfWork.TripRepository.GetTripWithDetailsAsync(id);
            if (trip == null)
                return NotFound();

            var currentTrip = new TripResponse
            {
                Id = trip.Id,
                Title = trip.Title,
                Description = trip.Description!,
                Price = trip.Price,
                ImageUrl = trip.ImageUrl,
                CountryName = trip.Country!.Name,
                AverageRating = trip.Reviews.Any() ? trip.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = trip.Reviews.Count,
                IsAvailable = trip.IsAvailable,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                DurationDays = trip.DurationDays,
                TripType = trip.TripType,
                TotalSeats = trip.TotalSeats,
                AvailableSeats = trip.AvailableSeats,
            };

            var relatedTrips = (await unitOfWork.TripRepository.GetRelatedTripsAsync(trip))
                                .Select(t => new TripResponse
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Price = t.Price,
                                    ImageUrl = t.ImageUrl,
                                    CountryName = t.Country!.Name,
                                });

            var reviews = trip.Reviews.Select(r => new ReviewResponse
            {
                UserName = r.User?.UserName ?? "Anonymous",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            });

            var vm = new TripDetailsViewModel
            {
                Trip = currentTrip,
                RelatedTrips = relatedTrips.ToList(),
                Reviews = reviews.ToList(),
                TripImages = trip.TripImages.Select(i => i.ImageUrl).ToList()
            };

            return View(vm);
        
        }

        
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null)
                return Unauthorized();

            var trip = await unitOfWork.TripRepository.GetOneAsync(t => t.Id == request.TripId);
            if (trip == null)
                return NotFound();

            if (trip.AvailableSeats < request.NumberOfPassengers)
                return BadRequest();

            var existingCartItem = await unitOfWork.TripCartRepository.GetOneAsync(
                c => c.UserId == user.Id && c.TripId == request.TripId
            );

            if (existingCartItem != null)
                existingCartItem.NumberOfPassengers++;
            else
            {
                var cartItem = new TripCart
                {
                    TripId = trip.Id,
                    UserId = user.Id,
                    NumberOfPassengers = request.NumberOfPassengers,
                    AddedAt = DateTime.UtcNow
                };
                await unitOfWork.TripCartRepository.CreateAsync(cartItem);
            }

            await unitOfWork.CommitAsync();
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(AddToWishlistRequest request)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null)
                return Unauthorized();

            var trip = await unitOfWork.TripRepository.GetOneAsync(t => t.Id == request.TripId);
            if (trip == null)
                return NotFound();

            var existingWishlistItem = await unitOfWork.TripWishlistRepository.GetOneAsync(
                w => w.UserId == user.Id && w.TripId == request.TripId
            );

            if (existingWishlistItem != null)
                return BadRequest();

            var wishlistItem = new TripWishlist
            {
                TripId = request.TripId,
                UserId = user.Id,
                AddedAt = DateTime.UtcNow
            };

            await unitOfWork.TripWishlistRepository.CreateAsync(wishlistItem);
            return RedirectToAction("Details", new { id = request.TripId });
        }

        // POST: Customer/Trips/Review
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewRequest request)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null)
                return Unauthorized();

            var trip = await unitOfWork.TripRepository.GetOneAsync(
                t => t.Id == request.TripId,
                includes: t => t.Include(t => t.Reviews)
            );

            if (trip == null)
                return NotFound();

            var alreadyReviewed = trip.Reviews.Any(r => r.UserId == user.Id);
            if (alreadyReviewed)
                return BadRequest();

            var review = new Review
            {
                TripId = trip.Id,
                UserId = user.Id,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.ReviewRepository.CreateAsync(review);
            await unitOfWork.CommitAsync();

            return RedirectToAction("Details", new { id = request.TripId });
        }

        // GET + POST: Customer/Trips/Search
        [HttpGet]
        public IActionResult Search() => View();

        [HttpPost]
        public async Task<IActionResult> Search(TripSearchRequest request, int pageNumber = 1, int pageSize = 10,
                                                string sortBy = "price", string sortOrder = "asc")
        {
            var trips = await unitOfWork.TripRepository.GetAsync(
                t => t.IsAvailable &&
                     t.AvailableSeats >= request.NumberOfPassengers &&
                     t.Country != null &&
                     t.Country.Name.ToLower().Contains(request.CountryName ?? "".ToLower()),
                includes: q => q.Include(t => t.Country).Include(t => t.Reviews)
            );

            if (request.DesiredDate.HasValue)
            {
                var twoWeeksBefore = request.DesiredDate.Value.AddDays(-14);
                var twoWeeksAfter = request.DesiredDate.Value.AddDays(14);
                trips = trips.Where(t => t.StartDate >= twoWeeksBefore && t.StartDate <= twoWeeksAfter);
            }

            var response = trips.Select(t => new TripResponse
            {
                Id = t.Id,
                Title = t.Title,
                CountryName = t.Country!.Name,
                StartDate = t.StartDate,
                Price = t.Price,
                ImageUrl = t.ImageUrl,
                DurationDays = t.DurationDays,
                IsAvailable = t.IsAvailable,
                AverageRating = t.Reviews != null && t.Reviews.Any()
                    ? Math.Round(t.Reviews.Average(r => r.Rating), 1)
                    : 0
            });

            response = sortBy.ToLower() switch
            {
                "price" => sortOrder.ToLower() == "desc" ? response.OrderByDescending(r => r.Price) : response.OrderBy(r => r.Price),
                "rating" => sortOrder.ToLower() == "desc" ? response.OrderByDescending(r => r.AverageRating) : response.OrderBy(r => r.AverageRating),
                _ => response.OrderBy(r => r.Price)
            };

            var totalCount = response.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedData = response.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var result = new TripSearchResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = paginatedData
            };

            return View("SearchResults", result);
        }
    }
}



