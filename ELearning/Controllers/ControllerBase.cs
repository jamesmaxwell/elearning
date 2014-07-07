using ServiceStack.Caching;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;

namespace ELearning.Controllers
{
    [ProfilingActionFilter]
    public class ControllerBase : ServiceStackController
    {
        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("elearning");
            }
        }
    }
}