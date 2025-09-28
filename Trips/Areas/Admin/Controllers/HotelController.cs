using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class HotelController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HotelController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null, string filter = "all")
        {
            var hotels = await unitOfWork.HotelRepository.GetAsync(
                includes: h => h
                    .Include(h => h.Country)
                    .Include(h => h.Trip)
                    .Include(h => h.Bookings)
            );

            
            if (!string.IsNullOrWhiteSpace(search))
            {
                hotels = hotels.Where(h =>
                    h.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    h.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (h.Country != null && h.Country.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (h.Trip != null && h.Trip.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            if (filter == "available")
            {
                hotels = hotels.Where(h => h.AvailableRooms > 0).ToList();
            }
            else if (filter == "full")
            {
                hotels = hotels.Where(h => h.AvailableRooms == 0).ToList();
            }

            int pageSize = 6;
            var totalCount = hotels.Count();

            var pagedHotels = hotels
                .OrderBy(h => h.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new HotelIndexVM
            {
                Hotels = pagedHotels,
                AvailableHotels = hotels.Count(h => h.AvailableRooms > 0),
                FullHotels = hotels.Count(h => h.AvailableRooms == 0),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search,
                Filter = filter
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new Hotel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var created = await unitOfWork.HotelRepository.CreateAsync(hotel);
                if (created)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "❌ Failed to create hotel.");
            }

            await PopulateDropdowns();
            return View(hotel);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var hotel = await unitOfWork.HotelRepository.GetOneAsync(
                h => h.Id == id,
                includes: h => h
                    .Include(h => h.Country)
                    .Include(h => h.Trip)
            );

            if (hotel == null) return NotFound();

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel hotel, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbHotel = await unitOfWork.HotelRepository.GetOneAsync(h => h.Id == hotel.Id);
                if (dbHotel == null) return NotFound();

                dbHotel.Name = hotel.Name;
                dbHotel.Description = hotel.Description;
                dbHotel.Phone = hotel.Phone;
                dbHotel.AvailableRooms = hotel.AvailableRooms;
                dbHotel.PricePerNight = hotel.PricePerNight;
                dbHotel.City = hotel.City;
                dbHotel.MainImg = hotel.MainImg;
                dbHotel.CountryId = hotel.CountryId;
                dbHotel.TripId = hotel.TripId;

                await unitOfWork.HotelRepository.UpdateAsync(dbHotel);

                return RedirectToAction(nameof(Index), new { page, search });
            }

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var hotel = await unitOfWork.HotelRepository.GetOneAsync(h => h.Id == id);
            if (hotel == null) return NotFound();

            var deleted = await unitOfWork.HotelRepository.DeleteAsync(hotel);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete hotel. It might be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        private async Task PopulateDropdowns()
        {
            var countries = await unitOfWork.CountryRepository.GetAsync() ?? new List<Country>();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            var trips = await unitOfWork.TripRepository.GetAsync() ?? new List<Trip>();
            ViewBag.Trips = new SelectList(trips, "Id", "Title");
        }
    }
}
