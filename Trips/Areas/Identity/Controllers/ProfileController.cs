using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Trips.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProfileController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Edit()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }

            if (user == null) return NotFound();

            var model = user.Adapt<EditProfileRequest>();

            ViewBag.CurrentImage = user.ImgUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileRequest editProfileRequest)
        {
            if (!ModelState.IsValid)
                return View(editProfileRequest);

            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null) return NotFound();

            var upuser = await unitOfWork.UserManager.FindByIdAsync(user.Id);
            if (upuser == null) return NotFound();

            
            if (!string.IsNullOrWhiteSpace(editProfileRequest.FirstName)) upuser.FirstName = editProfileRequest.FirstName;
            if (!string.IsNullOrWhiteSpace(editProfileRequest.LastName)) upuser.LastName = editProfileRequest.LastName;

            if (!string.IsNullOrWhiteSpace(editProfileRequest.Email))
            {
                if (!new EmailAddressAttribute().IsValid(editProfileRequest.Email))
                {
                    ModelState.AddModelError("", "Invalid email format");
                    return View(editProfileRequest);
                }

                var emailOwner = await unitOfWork.UserManager.FindByEmailAsync(editProfileRequest.Email);
                if (emailOwner != null && emailOwner.Id != upuser.Id)
                {
                    ModelState.AddModelError("", "Email is already in use by another account");
                    return View(editProfileRequest);
                }

                upuser.Email = editProfileRequest.Email;
                upuser.NormalizedEmail = editProfileRequest.Email.ToUpper();
            }

            if (!string.IsNullOrWhiteSpace(editProfileRequest.PhoneNumber))
                upuser.PhoneNumber = editProfileRequest.PhoneNumber;

            if (editProfileRequest.Address != null)
                upuser.Address = editProfileRequest.Address;

            if (editProfileRequest.ImageUrl != null && editProfileRequest.ImageUrl.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(editProfileRequest.ImageUrl.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Invalid image format. Only JPG, PNG, and GIF are allowed.");
                    return View(editProfileRequest);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(imagesPath);

                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await editProfileRequest.ImageUrl.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(upuser.ImgUrl))
                {
                    var oldFilePath = Path.Combine(imagesPath, upuser.ImgUrl);
                    if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
                }

                upuser.ImgUrl = fileName;
            }

            var result = await unitOfWork.UserManager.UpdateAsync(upuser);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", string.Join(", ", result.Errors.Select(e => e.Description)));
                return View(editProfileRequest);
            }

            await unitOfWork.CommitAsync();
            TempData["SuccessMessage"] = "Profile updated successfully";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null) return Unauthorized();

            var bookings = await unitOfWork.BookingRepository.GetAsync(b => b.UserId == user.Id);

            var bookingsCount = bookings.Count();
            var hotelWishlistCount = (await unitOfWork.HotelWishlistRepository.GetAsync(h => h.UserId == user.Id)).Count();
            var tripWishlistCount = (await unitOfWork.TripWishlistRepository.GetAsync(h => h.UserId == user.Id)).Count();
            var flightWishlistCount = (await unitOfWork.FlightWishlistRepository.GetAsync(h => h.UserId == user.Id)).Count();

            var allwishlistCount = hotelWishlistCount + tripWishlistCount + flightWishlistCount;
            var flightsBookedCount = (await unitOfWork.BookingRepository.GetAsync(b => b.UserId == user.Id && b.FlightId > 0)).Count();
            var userReviewsCount = (await unitOfWork.ReviewRepository.GetAsync(r => r.UserId == user.Id)).Count();

            var model = new DashboardVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                ImgUrl = user.ImgUrl,
                Address = user.Address,
                BookingsCount = bookingsCount,
                AllwishlistCount = allwishlistCount,
                FlightsBookedCount = flightsBookedCount,
                UserReviewsCount = userReviewsCount,
                Bookings = bookings
            };

            return View(model);
        }

        public async Task<IActionResult> MyBookings()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null) return Unauthorized();

            var myBookings = await unitOfWork.BookingRepository.GetAsync(b => b.UserId == user.Id);
            if (myBookings == null || !myBookings.Any()) TempData["InfoMessage"] = "There are no bookings yet!";

            return View(myBookings);
        }

        public async Task<IActionResult> MyReviews()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null) return Unauthorized();

            var myReviews = await unitOfWork.ReviewRepository.GetAsync(r => r.UserId == user.Id);
            if (myReviews == null || !myReviews.Any()) TempData["InfoMessage"] = "There are no reviews yet!";

            return View(myReviews);
        }

        public async Task<IActionResult> MyWishlist()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user is null)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?? " ";
                user = await unitOfWork.UserManager.FindByIdAsync(userId);
            }
            if (user == null) return Unauthorized();

            var myTripsWishlist = (await unitOfWork.TripWishlistRepository.GetAsync(t => t.UserId == user.Id, includes: q=> q.Include(t=> t.Trip).ThenInclude(t=> t.Country))).ToList();
            var myFlightsWishlist = (await unitOfWork.FlightWishlistRepository.GetAsync(f => f.UserId == user.Id, includes: q => q.Include(f => f.Flight))).ToList();
            var myHotelsWishlist = (await unitOfWork.HotelWishlistRepository.GetAsync(h => h.UserId == user.Id, includes: q => q.Include(h=> h.Hotel))).ToList();

            if ((myTripsWishlist == null || !myTripsWishlist.Any()) &&
                (myFlightsWishlist == null || !myFlightsWishlist.Any()) &&
                (myHotelsWishlist == null || !myHotelsWishlist.Any()))
            {
                TempData["InfoMessage"] = "There are no wishlists yet!";
            }

            var model = new WishlistVM
            {
                MyTripsWishlist = myTripsWishlist ?? new List<TripWishlist>(),
                MyFlightsWishlist = myFlightsWishlist ?? new List<FlightWishlist>(),
                MyHotelsWishlist = myHotelsWishlist ?? new List<HotelWishlist>()
            };

            return View(model);
        }
    }

}
