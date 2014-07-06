using System.Web.Mvc;
using System.Configuration;
using ServiceStack;
using ServiceStack.Mvc;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;

namespace ELearning
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("ELearning", typeof(MyServices).Assembly) { }

        public override void Configure(Container container)
        {
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider)
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            });
        }
    }

    public class Hello
    {
        public string Name { get; set; }
    }

    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return request;
        }
    }
}