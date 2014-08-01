using ELearning.Identity;
using ELearning.Models;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace ELearning
{
    public class DbInit
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void InitData()
        {
            typeof(IdentityUser).AddAttributes(new PostCreateTableAttribute(
            "insert into el_users(id,passwordhash,username) values('u1','AHdogl4Q4+zlahVEUwc0fCNraypE95RTgnKikiOF4Ga+4jUPOakP6PFaSpO+VGli1Q==','admin');")); //密码：abc123

            typeof(IdentityRole).AddAttributes(new PostCreateTableAttribute(
                "insert into el_roles(id,name) values('r1','管理员'); insert into el_roles(id,name) values('r2','普通用户');"));

            typeof(UserRole).AddAttributes(new PostCreateTableAttribute(
               "insert into el_userRoles(userid, roleid) values('u1','r1');"));

            typeof(Privilege).AddAttributes(new PostCreateTableAttribute(
                @"insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p1','Account.View','1','用户查看','Account','用户管理',1);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p2','Account.Admin','1','用户维护','Account','用户管理',2);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p3','Role.View','1','角色查看','Role','角色管理',3);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p4','Role.Admin','1','角色管理','Role','角色管理',4);"));

            typeof(RolePrivilege).AddAttributes(new PostCreateTableAttribute(
                @"insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p1');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p2');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p3');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p4');"));
        }
    }
}