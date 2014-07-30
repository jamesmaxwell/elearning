using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;

            return View(identity.Claims);
        }
    }
}