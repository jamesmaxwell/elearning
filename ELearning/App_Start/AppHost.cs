using Funq;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using System.Configuration;
using System.Web.Mvc;
using ELearning.Services;
using ELearning.Repository;

namespace ELearning
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("ELearning", typeof(MyServices).Assembly) { }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig { HandlerFactoryPath = "api" });

            //注册Ioc
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider)
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            });

            container.Register<ICacheClient>(new MemoryCacheClient());

            //regieste service
            container.RegisterAutoWiredAs<AuthRepository, IAuthRepository>();
            container.RegisterAutoWiredAs<AuthService, IAuthService>();


            //set controller factory
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
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