using BlogTutorial.Core;
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
        private AccountManager _accountManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, AccountManager accountManager)
        {
            _signInManager = signInManager;
            _accountManager = accountManager;
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
        [ModelValidationFilter]
        public async Task<IdentityResult> SignUp([FromBody] RegistrationModel userModel)
        {
            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _accountManager.Register(user, userModel.Password);

            return result;
        }
    }
}
