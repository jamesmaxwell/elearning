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
        /// <param name="userId">用户id</param>
        /// <returns>权限列表</returns>
        List<Privilege> GetUserPrivileges(string userId);
    }

    public class AuthService : ServiceBase, IAuthService
    {
        public IAuthRepository AuthRepository { get; set; }

        public AuthService()
        {
        }

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
    }
}