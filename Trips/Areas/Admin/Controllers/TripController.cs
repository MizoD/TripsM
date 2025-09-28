using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class TripController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TripController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var trips = await unitOfWork.TripRepository.GetAsync(
                includes: t => t.Include(t => t.Country)
                                .Include(t => t.Bookings)
                                .Include(t => t.Flights)
                                .Include(t => t.Hotels)
                                .Include(t => t.Reviews)
            );

            if (!string.IsNullOrWhiteSpace(search))
            {
                trips = trips.Where(t =>
                    t.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (t.Description != null && t.Description.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (t.Country != null && t.Country.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            int pageSize = 6;
            var totalCount = trips.Count();

            var pagedTrips = trips
                .OrderByDescending(t => t.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new TripIndexVM
            {
                Trips = pagedTrips,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new Trip());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                var created = await unitOfWork.TripRepository.CreateAsync(trip);

                if (created)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "❌ Failed to create trip.");
            }

            await PopulateDropdowns();
            return View(trip);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var trip = await unitOfWork.TripRepository.GetOneAsync(
                t => t.Id == id,
                includes: t => t.Include(t => t.Country)
                                .Include(t => t.Bookings)
                                .Include(t => t.Flights)
                                .Include(t => t.Hotels)
                                .Include(t => t.Reviews)
            );

            if (trip == null) return NotFound();

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Trip trip, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbTrip = await unitOfWork.TripRepository.GetOneAsync(t => t.Id == trip.Id);
                if (dbTrip == null) return NotFound();

                dbTrip.Title = trip.Title;
                dbTrip.Description = trip.Description;
                dbTrip.TripType = trip.TripType;
                dbTrip.CountryId = trip.CountryId;
                dbTrip.ImageUrl = trip.ImageUrl;
                dbTrip.StartDate = trip.StartDate;
                dbTrip.EndDate = trip.EndDate;
                dbTrip.TotalSeats = trip.TotalSeats;
                dbTrip.AvailableSeats = trip.AvailableSeats;
                dbTrip.Price = trip.Price;
                dbTrip.IsAvailable = trip.IsAvailable;
                dbTrip.SecondryImages = trip.SecondryImages;
                dbTrip.VideoUrl = trip.VideoUrl;

                await unitOfWork.TripRepository.UpdateAsync(dbTrip);

                return RedirectToAction(nameof(Index), new { page, search });
            }

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var trip = await unitOfWork.TripRepository.GetOneAsync(t => t.Id == id);
            if (trip == null) return NotFound();

            var deleted = await unitOfWork.TripRepository.DeleteAsync(trip);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete trip. It may be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        private async Task PopulateDropdowns()
        {
            var countries = await unitOfWork.CountryRepository.GetAsync() ?? new List<Country>();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
        }
    }
}
