using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Trips.Models;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var countriesName = (await unitOfWork.CountryRepository.GetAsync())
                               .Select(a => a.Name);

            var hotelsName = (await unitOfWork.HotelRepository.GetAsync())
                                .Select(h => h.Name);

            var reviews = (await unitOfWork.ReviewRepository.GetAsync(includes: r=> r.Include(r=> r.User).Include(r=> r.Trip)))
                                .OrderByDescending(r => r.CreatedAt)
                                .Take(10);

            var hotels = (await unitOfWork.HotelRepository.GetAsync(includes: h=> h.Include(h=> h.Trip).Include(h=>h.Country)))
                                .OrderByDescending(h => h.Traffic)
                                .Take(12);

            var trips = (await unitOfWork.TripRepository.GetAsync(includes: t=> t.Include(t=> t.Country).Include(t=> t.Reviews).Include(t=> t.TripImages)))
                                .OrderByDescending(t => t.Rate)
                                .Take(8);

            var flights = (await unitOfWork.FlightRepository.GetAsync(includes: f=> f.Include(f=> f.Aircraft).Include(f=> f.ArrivalAirport)
                                                                        .Include(f=> f.DepartureAirport).Include(f=> f.Trip)))
                                .Where(f => f.DepartureTime > DateTime.Now)
                                .OrderBy(f => f.DepartureTime)
                                .Take(6);

            var model = new HomeVM
            {
                AirportsName = countriesName.ToList(),
                HotelsName = hotelsName.ToList(),
                Reviews = reviews.ToList(),
                Hotels = hotels.ToList(),
                FeaturedTrips = trips.ToList(),
                UpcomingFlights = flights.ToList()
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
