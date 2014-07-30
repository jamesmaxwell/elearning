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
using ServiceStack.Configuration;

namespace ELearning
{
    public class AppConfig
    {
        public AppConfig(IAppSettings appSettings)
        {
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("ELearning", typeof(MyServices).Assembly) { }

        public static AppConfig AppConfig;

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig { HandlerFactoryPath = "api" });

            //Register Typed Config some services might need to access
            var appSettings = new AppSettings();
            AppConfig = new AppConfig(appSettings);
            container.Register(AppConfig);

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

            //TODO: 根据配置，自动生成表和数据,而不是通过sql脚本来处理。

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