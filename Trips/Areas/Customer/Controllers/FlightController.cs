using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FlightController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public FlightController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(
    int pageNumber = 1, int pageSize = 6, string sortBy = "price", string sortOrder = "asc",
    string? from = null, string? country = null, DateTime? departureDate = null, DateTime? returnDate = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 6;

            var flights = await unitOfWork.FlightRepository.GetAsync(
                includes: f => f
                    .Include(f => f.Aircraft)
                    .Include(f => f.ArrivalAirport).ThenInclude(a => a.Country)
                    .Include(f => f.DepartureAirport).ThenInclude(a => a.Country)
                    .Include(f => f.Trip)
                    .Include(f => f.Bookings)
                    .Include(f => f.Seats));

            if (!string.IsNullOrWhiteSpace(from))
            {
                flights = flights.Where(f =>
                    f.DepartureAirport.Country.Name.Contains(from, StringComparison.OrdinalIgnoreCase) ||
                    f.DepartureAirport.Name.Contains(from, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                flights = flights.Where(f =>
                    f.ArrivalAirport.Country.Name.Contains(country, StringComparison.OrdinalIgnoreCase) ||
                    f.ArrivalAirport.Name.Contains(country, StringComparison.OrdinalIgnoreCase));
            }

            if (departureDate.HasValue)
            {
                var dayStart = departureDate.Value.Date;
                var dayEnd = departureDate.Value.Date.AddDays(7);
                flights = flights.Where(f => f.DepartureTime >= dayStart && f.DepartureTime <= dayEnd);
            }

            if (returnDate.HasValue)
            {
                var dayStart = returnDate.Value.Date;
                var dayEnd = returnDate.Value.Date.AddDays(7);
                flights = flights.Where(f => f.DepartureTime >= dayStart && f.DepartureTime <= dayEnd);
            }

            flights = sortBy.ToLower() switch
            {
                "price" => sortOrder == "desc"
                    ? flights.OrderByDescending(f => f.Price)
                    : flights.OrderBy(f => f.Price),
                "date" => sortOrder == "desc"
                    ? flights.OrderByDescending(f => f.DepartureTime)
                    : flights.OrderBy(f => f.DepartureTime),
                "duration" => sortOrder == "desc"
                    ? flights.OrderByDescending(f => f.Duration)
                    : flights.OrderBy(f => f.Duration),
                _ => flights.OrderBy(f => f.Price)
            };

            var totalCount = flights.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var data = flights.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            var model = new PaginatedFlightResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = data
            };

            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var flight = await unitOfWork.FlightRepository.GetOneAsync(f => f.Id == id,
                includes: f => f.Include(f => f.Aircraft)
                                .Include(f => f.ArrivalAirport).ThenInclude(a => a.Country)
                                .Include(f => f.DepartureAirport).ThenInclude(a => a.Country)
                                .Include(f => f.Trip)
                                .Include(f => f.Bookings)
                                .Include(f => f.Seats));

            if (flight == null)
                return NotFound();


            var relatedFlights = await unitOfWork.FlightRepository.GetAsync(
        f => f.Id != id &&
             (f.DepartureAirport.CountryId == flight.DepartureAirport.CountryId ||
              f.ArrivalAirport.CountryId == flight.ArrivalAirport.CountryId),
        includes: f => f
            .Include(r => r.DepartureAirport).ThenInclude(a=> a.Country)
            .Include(r => r.ArrivalAirport).ThenInclude(a => a.Country)
    );

            // ✅ Fallback: if no flights found in same country, check by airport
            if (!relatedFlights.Any())
            {
                relatedFlights = await unitOfWork.FlightRepository.GetAsync(
                    f => f.Id != id &&
                         (f.DepartureAirportId == flight.DepartureAirportId ||
                          f.ArrivalAirportId == flight.ArrivalAirportId),
                    includes: f => f
                        .Include(r => r.DepartureAirport).ThenInclude(a => a.Country)
                        .Include(r => r.ArrivalAirport).ThenInclude(a => a.Country)
                );
            }

            var vm = new FlightDetailsViewVM
            {
                Flight = flight,
                RelatedFlights = relatedFlights.Take(4).ToList(),
                TotalSeats = flight.Seats.Count,
                BookedSeats = flight.Seats.Count(s => s.IsBooked),
                AvailableSeats = flight.Seats.Count(s => !s.IsBooked),
                Seats = flight.Seats.OrderBy(s => s.SeatLabel).ToList() 
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartWithSeats(int flightId, DateTime travelDate, string coach, string selectedSeatNumbers)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (travelDate < DateTime.Today)
            {
                TempData["Error"] = "Invalid Travel Date.";
                return RedirectToAction("Details", new { id = flightId });
            }

            var flight = await unitOfWork.FlightRepository.GetOneAsync(
                f => f.Id == flightId,
                includes: f => f.Include(f => f.Seats));

            if (flight == null)
                return NotFound();

            var seatNumbers = selectedSeatNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var numberOfPassengers = seatNumbers.Length;

            if (numberOfPassengers == 0)
            {
                TempData["Error"] = "Please select at least one seat.";
                return RedirectToAction("Details", new { id = flightId });
            }

            var unavailableSeats = new List<string>();
            foreach (var seatNumber in seatNumbers)
            {
                var seat = flight.Seats.FirstOrDefault(s => s.SeatLabel == seatNumber && !s.IsBooked);
                if (seat == null)
                {
                    unavailableSeats.Add(seatNumber);
                }
            }

            if (unavailableSeats.Any())
            {
                TempData["Error"] = $"Seats {string.Join(", ", unavailableSeats)} are no longer available.";
                return RedirectToAction("Details", new { id = flightId });
            }

            foreach (var seatNumber in seatNumbers)
            {
                var cartItem = new FlightCart
                {
                    FlightId = flight.Id,
                    UserId = user.Id,
                    NumberOfPassengers = 1, 
                    TravelDate = travelDate,
                    Coach = Enum.Parse<Coach>(coach),
                    SeatNumber = seatNumber,
                    AddedAt = DateTime.UtcNow
                };

                var added = await unitOfWork.FlightCartRepository.CreateAsync(cartItem);
                if (!added)
                {
                    TempData["Error"] = "Something went wrong while adding to cart.";
                    return RedirectToAction("Details", new { id = flightId });
                }

                var seat = flight.Seats.First(s => s.SeatLabel == seatNumber);
                seat.IsBooked = true;
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = $"{numberOfPassengers} seat{(numberOfPassengers > 1 ? "s" : "")} added to cart successfully!";
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int flightId, DateTime travelDate, int numberOfPassengers)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            if (travelDate < DateTime.Today)
            {
                TempData["Error"] = "Invalid Travel Date.";
                return RedirectToAction("Details", new { id = flightId });
            }

            var flight = await unitOfWork.FlightRepository.GetOneAsync(t => t.Id == flightId);

            if (flight == null)
                return NotFound();

            if (flight.AvailableSeats < numberOfPassengers)
            {
                TempData["Error"] = "Not enough available seats.";
                return RedirectToAction("Details", new { id = flightId });
            }

            var existingCartItem = await unitOfWork.FlightCartRepository.GetOneAsync(
                c => c.UserId == user.Id && c.FlightId == flightId);

            if (existingCartItem != null)
            {
                existingCartItem.NumberOfPassengers += numberOfPassengers;
            }
            else
            {
                var cartItem = new FlightCart
                {
                    FlightId = flight.Id,
                    UserId = user.Id,
                    NumberOfPassengers = numberOfPassengers,
                    AddedAt = DateTime.UtcNow
                };

                var added = await unitOfWork.FlightCartRepository.CreateAsync(cartItem);
                if (!added)
                {
                    TempData["Error"] = "Something went wrong while adding to cart.";
                    return RedirectToAction("Details", new { id = flightId });
                }
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = "Flight added to cart successfully.";
            return RedirectToAction("Index");
        }

        // ✅ Add to Wishlist
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int flightId)
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            var flight = await unitOfWork.FlightRepository.GetOneAsync(t => t.Id == flightId);

            if (flight == null)
                return NotFound();

            var existingWishlistItem = await unitOfWork.FlightWishlistRepository.GetOneAsync(
                w => w.UserId == user.Id && w.FlightId == flightId);

            if (existingWishlistItem != null)
            {
                TempData["Error"] = "This flight is already in your wishlist.";
                return RedirectToAction("Details", new { id = flightId });
            }

            var wishlistItem = new FlightWishlist
            {
                FlightId = flightId,
                UserId = user.Id,
                AddedAt = DateTime.UtcNow
            };

            var added = await unitOfWork.FlightWishlistRepository.CreateAsync(wishlistItem);
            if (!added)
            {
                TempData["Error"] = "Something went wrong while adding to wishlist.";
                return RedirectToAction("Details", new { id = flightId });
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = "Flight added to wishlist successfully.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Search(FlightSearchRequest request, int pageNumber = 1, int pageSize = 6, string sortBy = "price", string sortOrder = "asc")
        {
            var flights = await unitOfWork.FlightRepository.GetAsync(f =>
                    f.AvailableSeats >= request.NumberOfPassengers &&
                    f.DepartureTime > DateTime.UtcNow &&
                    f.Status == FlightStatus.Scheduled &&
                    f.ArrivalAirport.Country.Name.ToLower().Contains(request.Country.ToLower() ?? ""),
                includes: f => f
                    .Include(f => f.Aircraft)
                    .Include(f => f.ArrivalAirport)
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.Trip)
                    .Include(f => f.Bookings)
                    .Include(f => f.Seats));

            if (request.TravelDate.HasValue)
            {
                var dayBefore = request.TravelDate.Value.AddDays(-14);
                var dayAfter = request.TravelDate.Value.AddDays(14);
                flights = flights.Where(f => f.DepartureTime >= dayBefore && f.DepartureTime <= dayAfter);
            }

            // Sorting
            flights = sortBy.ToLower() switch
            {
                "price" => sortOrder.ToLower() == "desc"
                    ? flights.OrderByDescending(r => r.Price)
                    : flights.OrderBy(r => r.Price),
                _ => flights.OrderBy(r => r.Price)
            };

            // Pagination
            var totalCount = flights.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedData = flights
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new FlightSerachResponse
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
