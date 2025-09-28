using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var countries = await unitOfWork.CountryRepository
                .GetAsync(includes: c => c.Include(c => c.Trips).Include(c => c.Airports));

            if (!string.IsNullOrWhiteSpace(search))
            {
                countries = countries
                    .Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int pageSize = 6;
            var totalCount = countries.Count();

            var pagedCountries = countries
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new CountryIndexVM
            {
                Countries = pagedCountries,
                TotalAirports = countries.Sum(c => c.Airports.Count),
                TotalTrips = countries.Sum(c => c.Trips.Count),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new Country());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                var created = await unitOfWork.CountryRepository.CreateAsync(country);
                if (created)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "❌ Failed to create country.");
            }
            return View(country);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var country = await unitOfWork.CountryRepository.GetOneAsync(c => c.Id == id);
            if (country == null) return NotFound();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbCountry = await unitOfWork.CountryRepository.GetOneAsync(c => c.Id == country.Id);
                if (dbCountry == null) return NotFound();

                dbCountry.Name = country.Name;
                dbCountry.Code = country.Code;
                dbCountry.Currency = country.Currency;

                var updated = await unitOfWork.CountryRepository.UpdateAsync(dbCountry);
                if (updated)
                {
                    TempData["Success"] = $"✏️ Country '{dbCountry.Name}' updated successfully!";
                    return RedirectToAction(nameof(Index), new { page, search });
                }

                TempData["Error"] = "❌ Failed to update country.";
            }

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var country = await unitOfWork.CountryRepository.GetOneAsync(c => c.Id == id);

            if (country == null) return NotFound();

            var deleted = await unitOfWork.CountryRepository.DeleteAsync(country);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete country. It might be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }
    }
}
