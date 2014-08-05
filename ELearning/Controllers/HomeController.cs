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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}