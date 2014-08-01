using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ELearning.Services;
using ELearning.Common;
using ELearning.Models;

namespace ELearning.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        private AuthService _authService;

        public HomeController(AuthService authService)
        {
            _authService = authService;
        }

        // GET: Home
        public ActionResult Index()
        {
            //var identity = User.Identity as ClaimsIdentity;
            var menus = _authService.GetMenusByUserName(User.Identity.Name);


            return View(menus);
        }
    }
}