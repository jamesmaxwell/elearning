using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using ServiceStack;

namespace ELearning.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims != null)
                ViewBag.Claims = claims;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}