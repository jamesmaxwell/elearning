﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ELearning.Services;
using Funq;
using Microsoft.AspNet.Identity;
using ELearning.Identity;
using ELearning.Models;

namespace ELearning.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            var container = HttpContext.Current.Application["FunqContainer"] as Container;
            if (container == null)
                throw new InvalidOperationException("容器没有正确初始化。");

            var authService = container.Resolve<IAuthService>();
            var privileges = authService.GetUserPrivileges(userIdentity.Name);
            var privClaim = new Claim(ClaimTypes.Authentication, "");
            foreach (var privilege in privileges)
                privClaim.Properties.Add(privilege.PrivilegeType, privilege.PrivilegeValue);

            userIdentity.AddClaim(privClaim);

            return userIdentity;
        }
    }
}