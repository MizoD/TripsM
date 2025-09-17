using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class AirportController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AirportController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var airports = await unitOfWork.AirportRepository
                .GetAsync(includes: a => a.Include(a => a.Country));

            if (!string.IsNullOrWhiteSpace(search))
            {
                airports = airports
                    .Where(a => a.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int pageSize = 6;
            var totalCount = airports.Count();

            var pagedAirports = airports
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new AirportIndexVM
            {
                Airports = pagedAirports,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                ActiveAirports = airports.Count(a => a.IsActive),
                InactiveAirports = airports.Count(a => !a.IsActive),
                Search = search
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var countries = await unitOfWork.CountryRepository.GetAsync();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Airport airport)
        {
            if (ModelState.IsValid)
            {
                var created = await unitOfWork.AirportRepository.CreateAsync(airport);
                if (created)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "❌ Failed to create airport.");
            }

            var countries = await unitOfWork.CountryRepository.GetAsync();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return View(airport);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var airport = await unitOfWork.AirportRepository.GetOneAsync(a => a.Id == id);
            if (airport == null) return NotFound();

            var countries = await unitOfWork.CountryRepository.GetAsync();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            // Store return params in ViewBag
            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(airport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Airport airport, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbAirport = await unitOfWork.AirportRepository.GetOneAsync(a => a.Id == airport.Id);
                if (dbAirport == null) return NotFound();

                dbAirport.Name = airport.Name;
                dbAirport.City = airport.City;
                dbAirport.CountryId = airport.CountryId;
                dbAirport.IsActive = airport.IsActive;

                await unitOfWork.AirportRepository.UpdateAsync(dbAirport);

                // Redirect with page & search preserved
                return RedirectToAction(nameof(Index), new { page, search });
            }

            var countries = await unitOfWork.CountryRepository.GetAsync();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(airport);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var airport = await unitOfWork.AirportRepository
                .GetOneAsync(a => a.Id == id);

            if (airport == null) return NotFound();

            var deleted = await unitOfWork.AirportRepository.DeleteAsync(airport);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete airport. It might be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        [HttpPost]
        public async Task<IActionResult> Status(int id, int page = 1, string? search = null)
        {
            var airport = await unitOfWork.AirportRepository.GetOneAsync(a => a.Id == id);
            if (airport == null)
            {
                TempData["Error"] = "❌ Airport not found!";
                return RedirectToAction(nameof(Index), new { page, search });
            }

            airport.IsActive = !airport.IsActive;
            await unitOfWork.AirportRepository.UpdateAsync(airport);
            await unitOfWork.CommitAsync();

            TempData["Success"] = airport.IsActive
                ? $"✅ Airport '{airport.Name}' activated successfully!"
                : $"⏸️ Airport '{airport.Name}' deactivated successfully!";

            return RedirectToAction(nameof(Index), new { page, search });
        }

    }
}
