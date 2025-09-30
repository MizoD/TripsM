using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Response;
using System.Security.Claims;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var flightItems = (await unitOfWork.FlightCartRepository
                .GetAsync(fc => fc.UserId == userId,
                    q => q.Include(x => x.Flight)
                          .ThenInclude(f => f.DepartureAirport)
                          .Include(x => x.Flight)
                          .ThenInclude(f => f.ArrivalAirport)))
                .Select(fc => new CartItemResponse
                {
                    Type = "Flight",
                    ItemId = fc.FlightId,
                    Title = fc.Flight.Title,
                    PassengersOrRooms = fc.NumberOfPassengers,
                    // FIXED: Use the actual seat price based on coach class
                    Price = fc.Flight.GetPriceForCoach(fc.Coach),
                    AddedAt = fc.AddedAt,
                    TravelDate = fc.TravelDate,
                    Coach = fc.Coach.ToString(),
                    SeatNumber = fc.SeatNumber,
                    DepartureAirport = fc.Flight.DepartureAirport?.Name,
                    ArrivalAirport = fc.Flight.ArrivalAirport?.Name,
                    DepartureTime = fc.Flight.DepartureTime
                }).ToList();

            var tripItems = (await unitOfWork.TripCartRepository
                .GetAsync(tc => tc.UserId == userId, q => q.Include(x => x.Trip)))
                .Select(tc => new CartItemResponse
                {
                    Type = "Trip",
                    ItemId = tc.TripId,
                    Title = tc.Trip.Title,
                    PassengersOrRooms = tc.NumberOfPassengers,
                    Price = tc.NumberOfPassengers * tc.Trip.Price,
                    AddedAt = tc.AddedAt
                }).ToList();

            var hotelItems = (await unitOfWork.HotelCartRepository
                .GetAsync(hc => hc.UserId == userId, q => q.Include(x => x.Hotel)))
                .Select(hc => new CartItemResponse
                {
                    Type = "Hotel",
                    ItemId = hc.HotelId,
                    Title = hc.Hotel.Name,
                    PassengersOrRooms = hc.NumberOfPassengers,
                    Price = hc.NumberOfPassengers * hc.Hotel.PricePerNight,
                    AddedAt = hc.AddedAt
                }).ToList();

            var response = new CartIndexResponse
            {
                Items = flightItems
                        .Concat(tripItems)
                        .Concat(hotelItems)
                        .OrderByDescending(i => i.AddedAt)
                        .ToList()
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string type, int itemId, string? seatNumber = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            switch (type.ToLower())
            {
                case "flight":
                case "flights":
                    if (string.IsNullOrEmpty(seatNumber))
                    {
                        // Remove all seats for this flight
                        var flightSeats = await unitOfWork.FlightCartRepository
                            .GetAsync(x => x.UserId == userId && x.FlightId == itemId);
                        foreach (var seat in flightSeats)
                        {
                            await unitOfWork.FlightCartRepository.DeleteAsync(seat);
                        }
                    }
                    else
                    {
                        // Remove specific seat
                        var flight = await unitOfWork.FlightCartRepository
                            .GetOneAsync(x => x.UserId == userId && x.FlightId == itemId && x.SeatNumber == seatNumber);
                        if (flight == null) return NotFound();
                        await unitOfWork.FlightCartRepository.DeleteAsync(flight);
                    }
                    break;

                case "trip":
                case "trips":
                    var trip = await unitOfWork.TripCartRepository
                        .GetOneAsync(x => x.UserId == userId && x.TripId == itemId);
                    if (trip == null) return NotFound();
                    await unitOfWork.TripCartRepository.DeleteAsync(trip);
                    break;

                case "hotel":
                case "hotels":
                    var hotel = await unitOfWork.HotelCartRepository
                        .GetOneAsync(x => x.UserId == userId && x.HotelId == itemId);
                    if (hotel == null) return NotFound();
                    await unitOfWork.HotelCartRepository.DeleteAsync(hotel);
                    break;

                default:
                    return BadRequest("Invalid cart type. Use: flight, trip, hotel.");
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = $"{type} removed from cart.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var flights = await unitOfWork.FlightCartRepository.GetAsync(c => c.UserId == userId);
            var trips = await unitOfWork.TripCartRepository.GetAsync(c => c.UserId == userId);
            var hotels = await unitOfWork.HotelCartRepository.GetAsync(c => c.UserId == userId);

            foreach (var f in flights) await unitOfWork.FlightCartRepository.DeleteAsync(f);
            foreach (var t in trips) await unitOfWork.TripCartRepository.DeleteAsync(t);
            foreach (var h in hotels) await unitOfWork.HotelCartRepository.DeleteAsync(h);

            await unitOfWork.CommitAsync();

            TempData["Success"] = "Cart cleared successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
