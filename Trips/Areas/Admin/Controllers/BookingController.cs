using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Booking
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Trip)
                .Include(b => b.Flight)
                .Include(b => b.Hotel)
                .ToListAsync();

            ViewBag.TotalBookingsMade = await _context.Bookings.SumAsync(b => (decimal?)b.TotalAmount ?? 0);
            ViewBag.TotalCount = bookings.Count;

            return View(bookings);
        }

        // GET: Admin/Booking/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Trip)
                .Include(b => b.Flight)
                .Include(b => b.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // GET: Admin/Booking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.BookingDate = DateTime.UtcNow;
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Admin/Booking/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // POST: Admin/Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var existingBooking = await _context.Bookings.FindAsync(id);
                if (existingBooking == null) return NotFound();

                // تحديث الحقول
                existingBooking.TotalAmount = booking.TotalAmount;
                existingBooking.NumberOfTickets = booking.NumberOfTickets;
                existingBooking.PaymentMethod = booking.PaymentMethod;
                existingBooking.PaymentId = booking.PaymentId;
                existingBooking.SessionId = booking.SessionId;
                existingBooking.UserId = booking.UserId;
                existingBooking.TripId = booking.TripId;
                existingBooking.FlightId = booking.FlightId;
                existingBooking.HotelId = booking.HotelId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Admin/Booking/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        // POST: Admin/Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Booking/ToggleStatus/5
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            bool isActive = booking.PaymentMethod != PaymentMethod.CASHONSITE;

            TempData["StatusMessage"] = $"Booking {(isActive ? "activated" : "deactivated")} based on payment method";
            return RedirectToAction(nameof(Index));
        }
    }
}
