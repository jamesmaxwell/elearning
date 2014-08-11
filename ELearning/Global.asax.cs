using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.MiniProfiler;

namespace ELearning
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LogManager.LogFactory = new Log4NetFactory(true);
            var host = new AppHost().Init();
            Application["FunqContainer"] = host.Container;
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                Profiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            Profiler.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var log = LogManager.LogFactory.GetLogger("Common");
            log.Error(sender.GetType().ToString(), HttpContext.Current.Server.GetLastError());
        }
    }
}
