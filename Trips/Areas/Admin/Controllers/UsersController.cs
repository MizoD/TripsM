using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Request.UserRequest;
using Models.DTOs.Response.UserResponse;

namespace Tripesso.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ✅ GET: admin/users
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var query = _userManager.Users.AsQueryable();

            var totalUsers = await query.CountAsync();
            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var userResponses = new List<UserResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userResponses.Add(new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address ?? "",
                    Roles = roles.ToList()
                });
            }

            var response = new PagedUserResponse
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalUsers = totalUsers,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize),
                Users = userResponses
            };

            return View(response);
        }

        // ✅ GET: admin/users/details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var response = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address ?? "",
                Roles = roles.ToList()
            };

            return View(response);
        }

        // ✅ GET: admin/users/create
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: admin/users/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                Address = request.Address,
                RegistrationDate = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(request);
            }

            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                ModelState.AddModelError("", "Invalid role");
                return View(request);
            }

            await _userManager.AddToRoleAsync(user, request.Role);

            return RedirectToAction(nameof(Index));
        }

        // ✅ GET: admin/users/edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var request = new UpdateUserRequest
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Roles = roles.ToList()
            };

            return View(request);
        }

        // ✅ POST: admin/users/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserRequest request)
        {
            if (!ModelState.IsValid) return View(request);

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null) return NotFound();

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Address = request.Address;

            await _userManager.UpdateAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, request.Roles);

            return RedirectToAction(nameof(Index));
        }

        // ✅ GET: admin/users/delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // ✅ POST: admin/users/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // ✅ POST: admin/users/block/5
        [HttpPost]
        public async Task<IActionResult> Block(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnd = DateTime.UtcNow.AddYears(100); // block "forever"
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // ✅ POST: admin/users/unblock/5
        [HttpPost]
        public async Task<IActionResult> Unblock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
