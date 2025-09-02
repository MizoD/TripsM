using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.Hotels
                .Include(h => h.Country)
                .Include(h => h.Trip)
                .ToListAsync();

            return View(hotels);
        }

        // ✅ Details
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Country)
                .Include(h => h.Trip)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return NotFound();

            return View(hotel);
        }

        // ✅ Create (GET)
        public IActionResult Create()
        {
            ViewData["Countries"] = _context.Countries.ToList();
            ViewData["Trips"] = _context.Trips.ToList();
            return View();
        }

        // ✅ Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // إعادة تحميل الـ Dropdowns عند وجود خطأ
            ViewData["Countries"] = _context.Countries.ToList();
            ViewData["Trips"] = _context.Trips.ToList();
            return View(hotel);
        }

        // ✅ Edit (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound();

            ViewData["Countries"] = _context.Countries.ToList();
            ViewData["Trips"] = _context.Trips.ToList();
            return View(hotel);
        }

        // ✅ Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hotel hotel)
        {
            if (id != hotel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Hotels.Any(e => e.Id == hotel.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // إعادة تحميل الـ Dropdowns عند وجود خطأ
            ViewData["Countries"] = _context.Countries.ToList();
            ViewData["Trips"] = _context.Trips.ToList();
            return View(hotel);
        }

        // ✅ Delete (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels
                .Include(h => h.Country)
                .Include(h => h.Trip)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                return NotFound();

            return View(hotel);
        }

        // ✅ Delete (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
