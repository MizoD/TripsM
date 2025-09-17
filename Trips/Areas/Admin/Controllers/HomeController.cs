using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await unitOfWork.BookingRepository.GetAsync();
            var trips = await unitOfWork.TripRepository.GetAsync();
            var flights = await unitOfWork.FlightRepository.GetAsync();
            var hotels = await unitOfWork.HotelRepository.GetAsync();
            var aircrafts = await unitOfWork.AirCraftRepository.GetAsync();
            var airports = await unitOfWork.AirportRepository.GetAsync();
            var countries = await unitOfWork.CountryRepository.GetAsync();
            var reviews = await unitOfWork.ReviewRepository.GetAsync();
            var users = unitOfWork.UserManager.Users.Count();

            var goodReviews = reviews.Count(r => r.Rating >= 4);
            var badReviews = reviews.Count(r => r.Rating <= 2);

            var viewModel = new AdminDashboardVM
            {
                BookingsCount = bookings.Count(),
                TripsCount = trips.Count(),
                FlightsCount = flights.Count(),
                HotelsCount = hotels.Count(),
                TotalBookingsMade = bookings.Sum(b => (decimal?)b.TotalAmount ?? 0),

                AircraftCount = aircrafts.Count(),
                AirportsCount = airports.Count(),
                CountriesCount = countries.Count(),

                TotalReviews = reviews.Count(),
                GoodReviewsCount = goodReviews,
                BadReviewsCount = badReviews,

                TotalUsers = users
            };

            return View(viewModel);
        }
    }
}
