using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Trips
        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips
                .Include(t => t.Country)
                .Include(t => t.Reviews)
                .ToListAsync();

            return View(trips);
        }

        // GET: Admin/Trips/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Country)
                .Include(t => t.Reviews)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        // GET: Admin/Trips/Create
        public IActionResult Create()
        {
            ViewData["Countries"] = _context.Countries.ToList();
            return View();
        }

        // POST: Admin/Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Countries"] = _context.Countries.ToList();
            return View(trip);
        }

        // GET: Admin/Trips/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
                return NotFound();

            ViewData["Countries"] = _context.Countries.ToList();
            return View(trip);
        }

        // POST: Admin/Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trip trip)
        {
            if (id != trip.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Countries"] = _context.Countries.ToList();
            return View(trip);
        }

        // GET: Admin/Trips/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var trip = await _context.Trips
                .Include(t => t.Country)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        // POST: Admin/Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
