using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class AircraftController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AircraftController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var aircrafts = await unitOfWork.AirCraftRepository.GetAsync(
                includes: a => a.Include(a => a.Airport)
            );

            if (!string.IsNullOrWhiteSpace(search))
            {
                aircrafts = aircrafts.Where(a =>
                    a.Model.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.AirlineName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (a.Airport != null && a.Airport.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            int pageSize = 6;
            var totalCount = aircrafts.Count();

            var pagedAircrafts = aircrafts
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new AircraftIndexVM
            {
                Aircrafts = pagedAircrafts,
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
            return View(new AirCraft());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AirCraft aircraft)
        {
            if (ModelState.IsValid)
            {
                var created = await unitOfWork.AirCraftRepository.CreateAsync(aircraft);

                if (created)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "❌ Failed to create aircraft.");
            }

            await PopulateDropdowns();
            return View(aircraft);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var aircraft = await unitOfWork.AirCraftRepository.GetOneAsync(
                a => a.Id == id,
                includes: a => a.Include(a => a.Airport)
            );

            if (aircraft == null) return NotFound();

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(aircraft);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AirCraft aircraft, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbAircraft = await unitOfWork.AirCraftRepository.GetOneAsync(a => a.Id == aircraft.Id);
                if (dbAircraft == null) return NotFound();

                dbAircraft.Model = aircraft.Model;
                dbAircraft.Capacity = aircraft.Capacity;
                dbAircraft.Status = aircraft.Status;
                dbAircraft.AirlineName = aircraft.AirlineName;
                dbAircraft.Type = aircraft.Type;
                dbAircraft.InitialPrice = aircraft.InitialPrice;
                dbAircraft.IsActive = aircraft.IsActive;
                dbAircraft.AirportId = aircraft.AirportId;

                await unitOfWork.AirCraftRepository.UpdateAsync(dbAircraft);

                return RedirectToAction(nameof(Index), new { page, search });
            }

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(aircraft);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var aircraft = await unitOfWork.AirCraftRepository.GetOneAsync(a => a.Id == id);
            if (aircraft == null) return NotFound();

            var deleted = await unitOfWork.AirCraftRepository.DeleteAsync(aircraft);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete aircraft. It may be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id, int page = 1, string? search = null)
        {
            var aircraft = await unitOfWork.AirCraftRepository.GetOneAsync(a => a.Id == id);
            if (aircraft == null) return NotFound();

            // Cycle through statuses: Ready → Busy → Maintainance → Ready
            aircraft.Status = aircraft.Status switch
            {
                AirCraftStatus.Ready => AirCraftStatus.Busy,
                AirCraftStatus.Busy => AirCraftStatus.Maintainance,
                AirCraftStatus.Maintainance => AirCraftStatus.Ready,
                _ => AirCraftStatus.Ready
            };

            await unitOfWork.AirCraftRepository.UpdateAsync(aircraft);

            return RedirectToAction(nameof(Index), new { page, search });
        }
       
        private async Task PopulateDropdowns()
        {
            var airports = await unitOfWork.AirportRepository.GetAsync() ?? new List<Airport>();
            ViewBag.Airports = new SelectList(airports, "Id", "Name");
        }
    }
}
