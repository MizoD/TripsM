using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ModelsVM.Response;
using System.Security.Claims;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartsController : Controller   // ✅ بدل ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Customer/Carts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            // Flights
            var flightItems = (await unitOfWork.FlightCartRepository
                .GetAsync(fc => fc.UserId == userId, q => q.Include(x => x.Flight)))
                .Select(fc => new CartItemResponse
                {
                    Type = "Flight",
                    ItemId = fc.FlightId,
                    Title = fc.Flight.Title,
                    PassengersOrRooms = fc.NumberOfPassengers,
                    Price = fc.NumberOfPassengers * fc.Flight.BasePrice,
                    AddedAt = fc.AddedAt
                }).ToList();

            // Trips
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

            // Hotels
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

            return View(response);   // ✅ بدال Ok(response)
        }

        // POST: Customer/Carts/Remove
        [HttpPost]
        public async Task<IActionResult> Remove(string type, int itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            switch (type.ToLower())
            {
                case "flight":
                case "flights":
                    var flight = await unitOfWork.FlightCartRepository
                        .GetOneAsync(x => x.UserId == userId && x.FlightId == itemId);
                    if (flight != null) await unitOfWork.FlightCartRepository.DeleteAsync(flight);
                    break;

                case "trip":
                case "trips":
                    var trip = await unitOfWork.TripCartRepository
                        .GetOneAsync(x => x.UserId == userId && x.TripId == itemId);
                    if (trip != null) await unitOfWork.TripCartRepository.DeleteAsync(trip);
                    break;

                case "hotel":
                case "hotels":
                    var hotel = await unitOfWork.HotelCartRepository
                        .GetOneAsync(x => x.UserId == userId && x.HotelId == itemId);
                    if (hotel != null) await unitOfWork.HotelCartRepository.DeleteAsync(hotel);
                    break;

                default:
                    TempData["Error"] = "Invalid cart type.";
                    break;
            }

            await unitOfWork.CommitAsync();
            TempData["Success"] = $"{type} removed from cart.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Customer/Carts/Clear
        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

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