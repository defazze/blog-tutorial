using BlogTutorial.Data.Constants;
using BlogTutorial.Data.Models;
using BlogTutorial2017.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<bool> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            return result.Succeeded;
        }

        [Authorize]
        public async Task Logoff()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpPost]
        public async Task<IdentityResult> SignUp([FromBody] ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, IdentityConstants.USER_ROLE);

            return result;
        }
    }
}
