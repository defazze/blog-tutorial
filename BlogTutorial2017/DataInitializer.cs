using BlogTutorial.Data;
using BlogTutorial.Data.Constants;
using BlogTutorial.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017
{
    public class DataInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<Role> _roleManager;

        public DataInitializer(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager, BlogDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if (!_userManager.Users.Any())
            {
                var adminRole = await _roleManager.FindByNameAsync(IdentityConstants.ADMINISTRATOR_ROLE);
                if (adminRole == null)
                {
                    adminRole = new Role { RoleName = IdentityConstants.ADMINISTRATOR_ROLE };
                    await _roleManager.CreateAsync(adminRole);
                }

                var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.ru", Password= "admin" };
                await _userManager.CreateAsync(admin);

               await _userManager.AddToRoleAsync(admin, IdentityConstants.ADMINISTRATOR_ROLE);
            }
        }
    }
}
