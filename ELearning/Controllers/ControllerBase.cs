using System.Web.Mvc;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;
using ELearning.Models;
using ELearning.Services;

namespace ELearning.Controllers
{
    [ProfilingActionFilter]
    [Authorize]
    public class ControllerBase : ServiceStackController
    {
        public IAuthService AuthService { get; set; }

        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("Controller");
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            ViewBag.AuthMenus = AuthService.GetMenusByUserName(User.Identity.Name);
        }
    }
}