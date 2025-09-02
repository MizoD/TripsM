﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccess;

namespace Utility.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if (!_roleManager.Roles.Any() && !_userManager.Users.Any())
            {
                _roleManager.CreateAsync(new(SD.SuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new(SD.Admin)).GetAwaiter().GetResult(); 
                _roleManager.CreateAsync(new(SD.Employee)).GetAwaiter().GetResult(); 
                _roleManager.CreateAsync(new(SD.Company)).GetAwaiter().GetResult(); 
                _roleManager.CreateAsync(new(SD.Customer)).GetAwaiter().GetResult(); 

                _userManager.CreateAsync(new()
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@Tourism.com",
                    EmailConfirmed = true,
                    RegistrationDate = DateTime.UtcNow,
                }, "Crazy@Admin7").GetAwaiter().GetResult(); 

                var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();
                if (user is not null)
                {
                    _userManager.AddToRoleAsync(user, SD.SuperAdmin).GetAwaiter().GetResult();
                }

            }
        }
    }
}
