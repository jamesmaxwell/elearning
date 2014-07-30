using System.Web.Mvc;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;

namespace ELearning.Controllers
{
    [ProfilingActionFilter]
    [Authorize]
    public class ControllerBase : ServiceStackController
    {
        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("Controller");
            }
        }
    }
}