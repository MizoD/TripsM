using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var reviews = await unitOfWork.ReviewRepository.GetAsync(
                includes: r => r.Include(r => r.User).Include(r => r.Trip)
            );

            if (!string.IsNullOrWhiteSpace(search))
            {
                reviews = reviews.Where(r =>
                    (r.User != null && r.User.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (r.Trip != null && r.Trip.Title.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (r.Comment != null && r.Comment.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            int pageSize = 6;
            var totalCount = reviews.Count();

            var pagedReviews = reviews
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ReviewIndexVM
            {
                Reviews = pagedReviews,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id, int? page, string? search)
        {
            var review = await unitOfWork.ReviewRepository.GetOneAsync(
                r => r.Id == id,
                includes: r => r.Include(r => r.User).Include(r => r.Trip)
            );

            if (review == null) return NotFound();

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review, int? page, string? search)
        {
            if (review.Rating < 1 || review.Rating > 5)
            {
                ModelState.AddModelError("", "❌ Rating must be between 1 and 5.");
            }

            if (ModelState.IsValid)
            {
                var dbReview = await unitOfWork.ReviewRepository.GetOneAsync(r => r.Id == review.Id);
                if (dbReview == null) return NotFound();

                dbReview.Rating = review.Rating;
                dbReview.Comment = review.Comment;
                dbReview.TripId = review.TripId;
                dbReview.UserId = review.UserId;

                await unitOfWork.ReviewRepository.UpdateAsync(dbReview);

                return RedirectToAction(nameof(Index), new { page, search });
            }

            await PopulateDropdowns();

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
        {
            var review = await unitOfWork.ReviewRepository.GetOneAsync(r => r.Id == id);
            if (review == null) return NotFound();

            var deleted = await unitOfWork.ReviewRepository.DeleteAsync(review);
            if (!deleted)
            {
                TempData["Error"] = "❌ Failed to delete review. It might be referenced by other records.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }
        private async Task PopulateDropdowns()
        {
            var users = unitOfWork.UserManager.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            var trips = await unitOfWork.TripRepository.GetAsync() ?? new List<Trip>();
            ViewBag.Trips = new SelectList(trips, "Id", "Title");
        }
    }
}
