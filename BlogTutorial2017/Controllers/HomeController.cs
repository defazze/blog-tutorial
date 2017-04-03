using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello world!";
        }

        [Authorize]
        public string SecureMethod()
        {
            return "OK!";
        }
    }
}
