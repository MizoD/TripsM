using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public class BookingController : Controller
        {
            private readonly IUnitOfWork unitOfWork;

            public BookingController(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public async Task<IActionResult> Index(int page = 1, string? search = null)
            {
                var bookings = await unitOfWork.BookingRepository
                    .GetAsync(includes: b => b
                        .Include(b => b.User)
                        .Include(b => b.Trip)
                        .Include(b => b.Flight)
                        .Include(b => b.Hotel));

                if (!string.IsNullOrWhiteSpace(search))
                {
                    bookings = bookings
                        .Where(b =>
                            (b.User != null && b.User.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (b.Trip != null && b.Trip.Title.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (b.Flight != null && b.Flight.Title.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (b.Hotel != null && b.Hotel.Name.Contains(search, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }

                int pageSize = 6;
                var totalCount = bookings.Count();

                var pagedBookings = bookings
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModel = new BookingIndexVM
                {
                    Bookings = pagedBookings,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Search = search
                };

                return View(viewModel);
            }

            public async Task<IActionResult> Create()
            {
                await PopulateDropdowns();
                return View(new Booking());
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Booking booking)
            {
                if (ModelState.IsValid)
                {
                    booking.BookingDate = DateTime.UtcNow;

                    var created = await unitOfWork.BookingRepository.CreateAsync(booking);
                    if (created)
                        return RedirectToAction(nameof(Index));

                    ModelState.AddModelError("", "❌ Failed to create booking.");
                }

                await PopulateDropdowns();
                return View(booking);
            }

            public async Task<IActionResult> Edit(int id, int? page, string? search)
            {
                var booking = await unitOfWork.BookingRepository
                    .GetOneAsync(b => b.Id == id,
                        includes: b => b.Include(b => b.User).Include(b => b.Trip).Include(b => b.Flight).Include(b => b.Hotel));

                if (booking == null) return NotFound();

                await PopulateDropdowns();

                ViewBag.Page = page;
                ViewBag.Search = search;

                return View(booking);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Booking booking, int? page, string? search)
            {
                if (ModelState.IsValid)
                {
                    var dbBooking = await unitOfWork.BookingRepository.GetOneAsync(b => b.Id == booking.Id);
                    if (dbBooking == null) return NotFound();

                    dbBooking.TotalAmount = booking.TotalAmount;
                    dbBooking.Tickets = booking.Tickets;
                    dbBooking.PaymentMethod = booking.PaymentMethod;
                    dbBooking.PaymentId = booking.PaymentId;
                    dbBooking.SessionId = booking.SessionId;
                    dbBooking.UserId = booking.UserId;
                    dbBooking.TripId = booking.TripId;
                    dbBooking.FlightId = booking.FlightId;
                    dbBooking.HotelId = booking.HotelId;

                    await unitOfWork.BookingRepository.UpdateAsync(dbBooking);

                    return RedirectToAction(nameof(Index), new { page, search });
                }

                await PopulateDropdowns();

                ViewBag.Page = page;
                ViewBag.Search = search;

                return View(booking);
            }

            [HttpPost]
            public async Task<IActionResult> Delete(int id, int page = 1, string? search = null)
            {
                var booking = await unitOfWork.BookingRepository.GetOneAsync(b => b.Id == id);
                if (booking == null) return NotFound();

                var deleted = await unitOfWork.BookingRepository.DeleteAsync(booking);
                if (!deleted)
                {
                    TempData["Error"] = "❌ Failed to delete booking. It might be referenced by other records.";
                }

                return RedirectToAction(nameof(Index), new { page, search });
            }

            [HttpPost]
            public async Task<IActionResult> Status(int id, int page = 1, string? search = null)
            {
                var booking = await unitOfWork.BookingRepository.GetOneAsync(b => b.Id == id);
                if (booking == null)
                {
                    TempData["Error"] = "❌ Booking not found!";
                    return RedirectToAction(nameof(Index), new { page, search });
                }

                
                if (booking.PaymentMethod == PaymentMethod.CASHONSITE)
                {
                    booking.Status = BookingStatus.Pending;
                }
                else
                {
                    booking.Status = BookingStatus.Paid;
                }

                await unitOfWork.BookingRepository.UpdateAsync(booking);

                TempData["Success"] = $"✅ Booking status updated to {booking.Status}";

                return RedirectToAction(nameof(Index), new { page, search });
            }

            private async Task PopulateDropdowns()
            {
            var users = unitOfWork.UserManager.Users.ToList(); 
            ViewBag.Users = new SelectList(users, "Id", "UserName");

            
            var trips = await unitOfWork.TripRepository.GetAsync() ?? new List<Trip>();
            ViewBag.Trips = new SelectList(trips, "Id", "Title");

            
            var flights = await unitOfWork.FlightRepository.GetAsync() ?? new List<Flight>();
            ViewBag.Flights = new SelectList(flights, "Id", "Title");

            
            var hotels = await unitOfWork.HotelRepository.GetAsync() ?? new List<Hotel>();
            ViewBag.Hotels = new SelectList(hotels, "Id", "Name");
        }
        }
    }


