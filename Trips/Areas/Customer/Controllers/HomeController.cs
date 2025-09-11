using Microsoft.AspNetCore.Mvc;
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

            var reviews = (await unitOfWork.ReviewRepository.GetAsync())
                                .OrderByDescending(r => r.CreatedAt);

            var hotels = (await unitOfWork.HotelRepository.GetAsync())
                                .OrderByDescending(h => h.Traffic);


            var model = new HomeVM
            {
                AirportsName = countriesName.ToList(),
                HotelsName = hotelsName.ToList(),
                Reviews = reviews.ToList(),
                Hotels = hotels.ToList()
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
