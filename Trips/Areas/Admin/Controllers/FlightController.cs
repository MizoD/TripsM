using Microsoft.AspNetCore.Mvc;

namespace Trips.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    namespace Trips.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public class FlightController : Controller
        {
            private readonly IUnitOfWork unitOfWork;

            public FlightController(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IActionResult> Index(int page = 1, string? search = null, string filter = "all")
            {
                var flights = await unitOfWork.FlightRepository.GetAsync(
                    includes: f => f
                        .Include(f => f.DepartureAirport)
                        .Include(f => f.ArrivalAirport)
                        .Include(f => f.Aircraft)
                        .Include(f => f.Trip)
                        .Include(f => f.Seats)
                        .Include(s => s.Bookings)
                );

                
                if (!string.IsNullOrWhiteSpace(search))
                {
                    flights = flights.Where(f =>
                        f.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        (f.DepartureAirport != null && f.DepartureAirport.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (f.ArrivalAirport != null && f.ArrivalAirport.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (f.Aircraft != null && f.Aircraft.Model.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (f.Trip != null && f.Trip.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                
                if (filter == "upcoming")
                {
                    flights = flights.Where(f => f.DepartureTime >= DateTime.UtcNow).ToList();
                }
                else if (filter == "past")
                {
                    flights = flights.Where(f => f.ArrivalTime < DateTime.UtcNow).ToList();
                }

                int pageSize = 6;
                var totalCount = flights.Count();

                var pagedFlights = flights
                    .OrderBy(f => f.DepartureTime)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModel = new FlightIndexVM
                {
                    Flights = pagedFlights,
                    UpcomingFlights = flights.Where(f => f.DepartureTime > DateTime.UtcNow).Count(),
                    PastFlights = flights.Where(f=> f.DepartureTime < DateTime.UtcNow).Count(),
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
                return View(new Flight());
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Flight flight)
            {
                if (ModelState.IsValid)
                {
                    var created = await unitOfWork.FlightRepository.CreateAsync(flight);
                    if (created)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "❌ Failed to create flight.");
                }

                await PopulateDropdowns();
                return View(flight);
            }

            public async Task<IActionResult> Edit(int id, int? page, string? search)
            {
                var flight = await unitOfWork.FlightRepository.GetOneAsync(
                    f => f.Id == id,
                    includes: f => f
                        .Include(f => f.DepartureAirport)
                        .Include(f => f.ArrivalAirport)
                        .Include(f => f.Aircraft)
                        .Include(f => f.Trip)
                );

                if (flight == null) return NotFound();

                await PopulateDropdowns();

                ViewBag.Page = page;
                ViewBag.Search = search;

                return View(flight);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Flight flight, int? page, string? search)
            {
                if (ModelState.IsValid)
                {
                    var dbFlight = await unitOfWork.FlightRepository.GetOneAsync(f => f.Id == flight.Id);
                    if (dbFlight == null) return NotFound();

                    dbFlight.Title = flight.Title;
                    dbFlight.Status = flight.Status;
                    dbFlight.DepartureTime = flight.DepartureTime;
                    dbFlight.ArrivalTime = flight.ArrivalTime;
                    dbFlight.AirCraftId = flight.AirCraftId;
                    dbFlight.DepartureAirportId = flight.DepartureAirportId;
                    dbFlight.ArrivalAirportId = flight.ArrivalAirportId;
                    dbFlight.TripId = flight.TripId;

                    await unitOfWork.FlightRepository.UpdateAsync(dbFlight);

                    return RedirectToAction(nameof(Index), new { page, search });
                }

                await PopulateDropdowns();

                ViewBag.Page = page;
                ViewBag.Search = search;

                return View(flight);
            }

            [HttpPost]
            public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
            {
                var flight = await unitOfWork.FlightRepository.GetOneAsync(f => f.Id == id);
                if (flight == null) return NotFound();

                var deleted = await unitOfWork.FlightRepository.DeleteAsync(flight);
                if (!deleted)
                {
                    TempData["Error"] = "❌ Failed to delete flight. It might be referenced by other records.";
                }

                return RedirectToAction(nameof(Index), new { page, search });
            }

            private async Task PopulateDropdowns()
            {
                var airports = await unitOfWork.AirportRepository.GetAsync() ?? new List<Airport>();
                ViewBag.Airports = new SelectList(airports, "Id", "Name");

                var aircrafts = await unitOfWork.AirCraftRepository.GetAsync() ?? new List<AirCraft>();
                ViewBag.Aircrafts = new SelectList(aircrafts, "Id", "Model");

                var trips = await unitOfWork.TripRepository.GetAsync() ?? new List<Trip>();
                ViewBag.Trips = new SelectList(trips, "Id", "Title");
            }
        }
    }

}
