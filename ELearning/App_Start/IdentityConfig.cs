﻿using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using ELearning.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ELearning.Models;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ELearning.Common;

namespace ELearning
{
    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            MsSQLDatabase database = null;
            var container = HttpContext.Current.Application["FunqContainer"] as Container;
            if (container != null)
            {
                var connFactory = container.Resolve<IDbConnectionFactory>();
                database = new MsSQLDatabase(connFactory);
            }
            else
            {
                database = new MsSQLDatabase();
            }

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(database));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并在此处插入。
            //manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is: {0}"
            //});
            //manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "安全代码",
            //    BodyFormat = "Your security code is: {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            MsSQLDatabase database = null;
            var container = HttpContext.Current.Application["FunqContainer"] as Container;
            if (container != null)
            {
                var connFactory = container.Resolve<IDbConnectionFactory>();
                database = new MsSQLDatabase(connFactory);
            }
            else
            {
                database = new MsSQLDatabase();
            }

            return new ApplicationRoleManager(new RoleStore(database));
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入短信服务可发送短信。
            return Task.FromResult(0);
        }
    }
}
