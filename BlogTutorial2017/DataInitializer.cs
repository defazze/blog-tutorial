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
            await GetOrCreateRole(IdentityConstants.ADMINISTRATOR_ROLE);
            await GetOrCreateRole(IdentityConstants.USER_ROLE);

            if (!_userManager.Users.Any())
            {
                var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.ru", Password= "administrator" };
                await _userManager.CreateAsync(admin, admin.Password);
               
               await _userManager.AddToRoleAsync(admin, IdentityConstants.ADMINISTRATOR_ROLE);
            }
        }

        private async Task<Role> GetOrCreateRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new Role { RoleName = roleName };
                await _roleManager.CreateAsync(role);
            }

            return role;
        }
    }
}
