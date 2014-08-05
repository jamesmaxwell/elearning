using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.OrmLite;
using ELearning.Repository;
using ELearning.Identity;
using ELearning.Models;
using WebGrease.Css.Extensions;

namespace ELearning.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// 根据用户id，取该用户的所有权限列表。
        /// 权限与角色相关，根据用户所对应的所有角色，获取所有权限。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>权限列表</returns>
        List<Privilege> GetUserPrivileges(string userName);

        /// <summary>
        /// 根据用户得取该用户相关的角色的所有授权菜单列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>菜单列表</returns>
        List<Menu> GetMenusByUserName(string userName);
    }

    public class AuthService : ServiceBase, IAuthService
    {
        public IAuthRepository AuthRepository { get; set; }

        public List<Privilege> GetUserPrivileges(string userName)
        {
            Log.DebugFormat("Get User Privileges: {0}", userName);

            var privilegs = new List<Privilege>();
            var pIdSet = AuthRepository.GetUserPrivilegeIds(userName);

            if (pIdSet.Count > 0)
            {
                var allPrivileges = Cache.Get<List<Privilege>>(Privilege.AllPrivilegeCacheKey);
                if (allPrivileges == null)
                {
                    allPrivileges = AuthRepository.GetAllPrivileges();
                    Cache.Add<List<Privilege>>(Privilege.AllPrivilegeCacheKey, allPrivileges);
                }

                pIdSet.ForEach(pId =>
                {
                    var privilege = allPrivileges.FirstOrDefault(p => p.Id == pId);
                    if (privilege != null)
                        privilegs.Add(privilege);
                });
            }

            return privilegs;
        }


        public List<Menu> GetMenusByUserName(string userName)
        {
            var menus = AuthRepository.GetMenusByUserName(userName).Distinct().OrderBy(x => x.Id).ToList();

            //TODO: cache user menu

            return menus;
        }
    }
}