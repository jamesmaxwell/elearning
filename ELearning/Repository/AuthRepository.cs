using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELearning.Models;
using ELearning.Identity;
using ServiceStack.OrmLite;

namespace ELearning.Repository
{
    public interface IAuthRepository
    {
        /// <summary>
        /// 读取所有授权项
        /// </summary>
        /// <returns></returns>
        List<Privilege> GetAllPrivileges();

        /// <summary>
        /// 根据用户名取得所有该用户包含角色的授权ID列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>不重复的授权ID列表</returns>
        HashSet<string> GetUserPrivilegeIds(string userName);

        /// <summary>
        /// 根据用户得取该用户相关的角色的所有授权菜单列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>菜单列表</returns>
        List<Menu> GetMenusByUserName(string userName);
    }

    public class AuthRepository : Repository, IAuthRepository
    {

        public List<Privilege> GetAllPrivileges()
        {
            using (var db = ConnFactory.Open())
            {
                return db.Select<Privilege>();
            }
        }

        public HashSet<string> GetUserPrivilegeIds(string userName)
        {
            using (var db = ConnFactory.Open())
            {
                return db.ColumnDistinct<string>(db.From<IdentityUser>().Select("EL_Privileges.Id")
                  .Join<UserRole>((user, role) => user.Id == role.UserId)
                  .Join<RolePrivilege, UserRole>((privilege, role) => privilege.RoleId == role.RoleId)
                  .Join<Privilege, RolePrivilege>((privilege, rolePrivilege) => privilege.Id == rolePrivilege.PrivilegeId)
                  .Where(x => x.UserName == userName)
              );
            }
        }


        public List<Menu> GetMenusByUserName(string userName)
        {
            using (var db = ConnFactory.Open())
            {
                return db.Select<Menu>(db.From<IdentityUser>().Select("DISTINCT EL_Menus.*")
                    .Join<IdentityRole, RoleMenu>((role, roleMenu) => role.Id == roleMenu.RoleId)
                    .Join<UserRole, IdentityRole>((userRole, role) => userRole.RoleId == role.Id)
                    .Join<IdentityUser, UserRole>((user, userRole) => user.Id == userRole.UserId)
                    .Join<Menu, RoleMenu>((menu, roleMenu) => menu.Id == roleMenu.MenuId)
                    .Where(x => x.UserName == userName)
                    );
            }
        }
    }
}