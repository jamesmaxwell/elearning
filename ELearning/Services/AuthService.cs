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

    public class AuthService : Service, IAuthService
    {
        private IAuthRepository _authRepository;
        //用户权限缓存key,以字典形式保存所有用户的权限列表
        private const string Auth_Cache_Key = "Auth_Cache_Key";

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public List<Privilege> GetUserPrivileges(string userName)
        {
            var privilegs = new List<Privilege>();
            var pIdSet = Db.ColumnDistinct<string>(Db.From<IdentityUser>().Select("EL_Privileges.Id")
                 .Join<UserRole>((user, role) => user.Id == role.UserId)
                 .Join<RolePrivilege, UserRole>((privilege, role) => privilege.RoleId == role.RoleId)
                 .Join<Privilege, RolePrivilege>((privilege, rolePrivilege) => privilege.Id == rolePrivilege.PrivilegeId)
                 .Where(x => x.UserName == userName)
                 );

            if (pIdSet.Count > 0)
            {
                var allPrivileges = Db.Select<Privilege>();
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