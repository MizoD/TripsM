using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ModelsVM;

namespace Trips.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class NewsletterController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public NewsletterController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(NewsletterVM model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home");

            var existing = await unitOfWork.SubscriberRepository.GetOneAsync(x => x.Email == model.Email);

            if (existing == null)
            {
                var subscriber = new NewsletterSubscriber
                {
                    Email = model.Email
                };
                await unitOfWork.SubscriberRepository.CreateAsync(subscriber);
            }

            TempData["SuccessMessage"] = "Thank you for subscribing!";
            return RedirectToAction("Index", "Home"); 
        }
    }
}
