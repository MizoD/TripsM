using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Trips.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            var query = unitOfWork.UserManager.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u =>
                    u.UserName!.Contains(search) ||
                    u.Email!.Contains(search) ||
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search));
            }

            int pageSize = 6;
            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new UserIndexVM
            {
                Users = users,
                TotalUsers = query.Count(),
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
            await PopulateRoles();
            return View(new ApplicationUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string password, string role)
        {
            if (ModelState.IsValid)
            {
                var existingEmailUser = await unitOfWork.UserManager.FindByEmailAsync(user.Email?? "");
                if (existingEmailUser != null)
                {
                    ModelState.AddModelError("Email", "❌ This email is already in use.");
                    await PopulateRoles();
                    return View(user);
                }

                var existingUserNameUser = await unitOfWork.UserManager.FindByNameAsync(user.UserName?? "");
                if (existingUserNameUser != null)
                {
                    ModelState.AddModelError("UserName", "❌ This username is already in use.");
                    await PopulateRoles();
                    return View(user);
                }

                user.RegistrationDate = DateTime.UtcNow;
                user.LastLogin = DateTime.UtcNow;

                var result = await unitOfWork.UserManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    if (await unitOfWork.RoleManager.RoleExistsAsync(role))
                    {
                        await unitOfWork.UserManager.AddToRoleAsync(user, role);
                        user.EmailConfirmed = true;
                        await unitOfWork.CommitAsync();

                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "❌ Invalid role.");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            await PopulateRoles();
            return View(user);
        }


        public async Task<IActionResult> Edit(string id, int? page, string? search)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await PopulateRoles();

            var userRoles = await unitOfWork.UserManager.GetRolesAsync(user);
            ViewBag.UserRoles = userRoles; 

            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user, string[] roles, int? page, string? search)
        {
            if (ModelState.IsValid)
            {
                var dbUser = await unitOfWork.UserManager.FindByIdAsync(user.Id);
                if (dbUser == null) return NotFound();

                var existingUserName = await unitOfWork.UserManager.FindByNameAsync(user.UserName ?? "");
                if (existingUserName != null && existingUserName.Id != user.Id)
                {
                    ModelState.AddModelError("UserName", "This username is already taken.");
                    await PopulateRoles();
                    ViewBag.Page = page;
                    ViewBag.Search = search;
                    return View(user);
                }

                var existingEmail = await unitOfWork.UserManager.FindByEmailAsync(user.Email ?? "");
                if (existingEmail != null && existingEmail.Id != user.Id)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    await PopulateRoles();
                    ViewBag.Page = page;
                    ViewBag.Search = search;
                    return View(user);
                }

                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.UserName = user.UserName;
                dbUser.Email = user.Email;
                dbUser.Address = user.Address;
                dbUser.ImgUrl = user.ImgUrl;

                var result = await unitOfWork.UserManager.UpdateAsync(dbUser);
                if (result.Succeeded)
                {
                    var currentRoles = await unitOfWork.UserManager.GetRolesAsync(dbUser);
                    await unitOfWork.UserManager.RemoveFromRolesAsync(dbUser, currentRoles);
                    await unitOfWork.UserManager.AddToRolesAsync(dbUser, roles);

                    return RedirectToAction(nameof(Index), new { page, search });
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            await PopulateRoles();
            ViewBag.Page = page;
            ViewBag.Search = search;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, int page = 1, string? search = null)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await unitOfWork.UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Error"] = "❌ Failed to delete user.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        [HttpPost]
        public async Task<IActionResult> Block(string id, int page = 1, string? search = null)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (user.LockoutEnabled && user.LockoutEnd == null)
            {
                user.LockoutEnd = DateTime.UtcNow.AddYears(100);
            }
            else
            {
                user.LockoutEnd = null;
            }

            var result = await unitOfWork.UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                TempData["Error"] = "❌ Failed to block/unblock user.";
            }

            return RedirectToAction(nameof(Index), new { page, search });
        }

        private async Task PopulateRoles()
        {
            var roles = await unitOfWork.RoleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
        }
    }
}
