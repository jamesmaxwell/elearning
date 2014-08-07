using ELearning.Identity;
using ELearning.Models;
using ServiceStack;
using ServiceStack.DataAnnotations;
using System.IO;
using ServiceStack.Logging;
using System.Text;

namespace ELearning
{
    public class DbInit
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        public static void InitData()
        {
            var log = LogManager.LogFactory.GetLogger("AppHost");

            //用户
            typeof(IdentityUser).AddAttributes(new PostCreateTableAttribute(
            "insert into el_users(id,passwordhash,username, RealName, Status, BelongsTo) values('u1','AHdogl4Q4+zlahVEUwc0fCNraypE95RTgnKikiOF4Ga+4jUPOakP6PFaSpO+VGli1Q==','admin','李一天',0,125);")); //密码：abc123

            //角色
            typeof(IdentityRole).AddAttributes(new PostCreateTableAttribute(
                @"insert into el_roles(id,name) values('r1','管理员');
                  insert into el_roles(id,name) values('r2','注册用户');"));

            //用户，角色
            typeof(UserRole).AddAttributes(new PostCreateTableAttribute(
               @"insert into el_userRoles(userid, roleid) values('u1','r1');
                 insert into el_userRoles(userid, roleid) values('u1','r2');"));

            //授权项
            typeof(Privilege).AddAttributes(new PostCreateTableAttribute(
                @"insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p1','Account.View','1','用户查看','Account','用户管理',1);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p2','Account.Admin','1','用户维护','Account','用户管理',2);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p3','Role.View','1','角色查看','Role','角色管理',3);
                insert into el_privileges(id,privilegetype,privilegevalue,privilegeName,groupCode,GroupName,PrivilegeIndex) 
                    values('p4','Role.Admin','1','角色管理','Role','角色管理',4);"));

            //角色授权项
            typeof(RolePrivilege).AddAttributes(new PostCreateTableAttribute(
                @"insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p1');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p2');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p3');
                  insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p4');"));

            //菜单和角色菜单对应关系
            typeof(Menu).AddAttributes(new PostCreateTableAttribute(
              @"insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(1,'考试中心',0,1,'Top','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(11,'考试管理',1,1,'Top','/Exam/Index');
                        insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(111,'新建考试',11,1,'Top','Exam/Create');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(12,'试卷管理',1,2,'Top','/Exam/PaperIndex');
                        insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(121,'添加试卷',12,2,'Top','/Exam/PaperCreate');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(13,'试题管理',1,3,'Top','/Exam/Question');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(2,'课程中心',0,2,'Top','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(21,'新建课程',2,1,'Top','/Course/Create');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(22,'课程管理',2,2,'Top','/Course/Index');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(23,'资源综合同步',2,3,'Top','/Course/ResSync');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(24,'课件资源同步',2,4,'Top','/Course/CsSync');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(3,'内容管理',0,3,'Top','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(31,'培训需求管理',3,1,'Top','/Content/Apply');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(32,'幻灯片管理',3,2,'Top','/Content/Slide');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(33,'我的建议管理',3,3,'Top','/Content/Suggest');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(34,'通知管理',3,4,'Top','/Content/Info');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(4,'系统管理',0,4,'Top','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(41,'分类管理',4,1,'Top','/SysAdmin/Category');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(5,'权限管理',0,5,'Top','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(51,'用户管理',5,1,'Top','/SysAdmin/Users');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(52,'角色管理',5,2,'Top','/SysAdmin/Roles');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(53,'用户导入',5,3,'Top','/SysAdmin/UserImp');
                /*
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(6,'我的考试',0,6,'Left','/Exam/Index');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(61,'我的任务考试',6,1,'Left','/Exam/MyTask');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(62,'我的推荐考试',6,2,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(63,'成绩查询',6,3,'Left','#');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(7,'我的学习',0,7,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(71,'最新课程',7,1,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(72,'课程浏览',7,2,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(73,'我的收藏',7,3,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(74,'已学习课程',7,4,'Left','#');
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(8,'我的信息',0,8,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(81,'系统通知',8,1,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(82,'我的建议',8,2,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(83,'行业信息',8,3,'Left','#');
                */
                insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(9,'我的网点',0,9,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(91,'网点账户',9,1,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(92,'网点考试情况',9,2,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(93,'网点学习情况',9,3,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(94,'培训需求',9,4,'Left','#');
                    insert into el_Menus(Id,Name,ParentId,Weight,Position,Url) values(95,'网点建议',9,5,'Left','#');
            "));

            //默认管理员角色有所有菜单权限,注册用户只有左侧菜单权限
            typeof(RoleMenu).AddAttributes(new PostCreateTableAttribute(
              @"insert into EL_RoleMenus(RoleId,MenuId) values('r1',1);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',2);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',3);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',4);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',5);
                /*
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',6);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',7);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',8);
                */
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',9);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',11);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',111);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',12);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',121);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',13);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',21);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',22);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',23);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',24);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',31);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',32);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',33);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',34);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',41);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',51);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',52);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',53);
                /*
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',61);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',62);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',63);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',71);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',72);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',73);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',74);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',81);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',82);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',83);
                */
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',91);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',92);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',93);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',94);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',95);
                /* 角色r2 的默认授权*/
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',1);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',2);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',11);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',111);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',12);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',121);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',13);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',21);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',22);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',23);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',24);
                /*
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',6);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',7);
                insert into EL_RoleMenus(RoleId,MenuId) values('r1',8);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',61);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',62);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',63);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',71);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',72);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',73);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',74);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',81);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',82);
                insert into EL_RoleMenus(RoleId,MenuId) values('r2',83);
                */
                "));

            //生成区域信息
            var path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/区域.csv");
            var sb = new StringBuilder(2048);
            var lines = File.ReadAllLines(path, Encoding.GetEncoding("GB2312"));
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                sb.AppendFormat("insert into EL_Areas(Id, Name, Description, ParentId) values({0},'{1}','{2}',{3});", parts[0], parts[1], parts[2], parts[3]);
            }
            typeof(AreaInfo).AddAttributes(new PostCreateTableAttribute(sb.ToString()));
        }
    }
}