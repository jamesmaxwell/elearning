using System.CodeDom;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;
using ELearning.Models;
using ELearning.ViewModels;
using ELearning.Attributes;
using ELearning.Services;
using ELearning.Common;

namespace ELearning.Controllers
{
    [ProfilingActionFilter]
    [Authorize]
    public class ControllerBase : ServiceStackController
    {
        public IAuthService AuthService { get; set; }

        protected ILog Log
        {
            get
            {
                return LogManager.LogFactory.GetLogger("Controller");
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //创建顶部菜单视图
            ViewBag.MenuViewModel = CreateMenu();

            //创建左边菜单视图
            ViewBag.SideMenus = CreateSideMenu(
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //创建用于面屑的视图对象
            ViewBag.ShowName = CreateShowName(filterContext);

            base.OnActionExecuted(filterContext);
        }

        #region helper methods
        private void CreateMenuNode(Node<Menu> root, List<Menu> menus, List<Menu> allMenus)
        {
            if (menus == null || menus.Count() == 0)
                return;

            foreach (var menu in menus)
            {
                var node = root.Add(menu);
                var subMenus = allMenus.Where(m => m.ParentId == menu.Id).ToList();
                CreateMenuNode(node, subMenus, allMenus);
            }
        }

        private MenuNodeViewModel CreateMenu()
        {
            var menus = AuthService.GetMenusByUserName(User.Identity.Name);
            var topRoots = menus.Where(m => m.Position == MenuPosition.Top && m.ParentId == 0).ToList();

            var menuVM = new MenuNodeViewModel();
            var topRoot = new Node<Menu>(new Menu { Id = -1 });

            CreateMenuNode(topRoot, topRoots, menus);
            menuVM.TopRoot = topRoot;

            return menuVM;
        }

        private List<SideMenu> CreateSideMenu(string currentController, string currentAction)
        {
            var sideMenus = new List<SideMenu>
            {
                new SideMenu{ Name="首页", Url="/Home/Index", IconClass = "icon-home", IsActive = currentController == "Home"},
                new SideMenu{Name="我的考试", Url="/Exam/Index", IconClass  ="icon-pencil", IsActive = currentController == "Exam", 
                    ChildMenus = new List<SideChildMenu>{
                        new SideChildMenu{ Name="我的任务考试", Url = "/Exam/MyTask", IsActive = currentAction == "MyTask" && currentController == "Exam"},
                        new SideChildMenu{ Name="我的推荐考试", Url = "/Exam/MyRec", IsActive = currentAction == "MyRec" && currentController == "Exam"},
                        new SideChildMenu{ Name="成绩查询", Url = "/Exam/Query", IsActive = currentAction == "Query" && currentController == "Exam"}
                    }},
                new SideMenu{Name="我的学习", Url="/Learn/Index", IconClass  ="icon-file", IsActive = currentController == "Learn", 
                    ChildMenus = new List<SideChildMenu>{
                        new SideChildMenu{ Name="最新课程", Url = "/Learn/NewCourse", IsActive = currentAction == "NewCourse" && currentController == "Learn"},
                        new SideChildMenu{ Name="课程浏览", Url = "/Learn/Course", IsActive = currentAction == "Course" && currentController == "Learn"},
                        new SideChildMenu{ Name="我的收藏", Url = "/Learn/Fav", IsActive = currentAction == "Fav" && currentController == "Learn"},
                        new SideChildMenu{ Name="已学习课程", Url = "/Learn/Studied", IsActive = currentAction == "Studied" && currentController == "Learn"}
                    }},
                 new SideMenu{Name="我的信息", Url="/Info/Index", IconClass  ="icon-user", IsActive = currentController == "Info", 
                    ChildMenus = new List<SideChildMenu>{
                        new SideChildMenu{ Name="系统通知", Url = "/Info/SysInfo", IsActive = currentAction == "SysInfo" && currentController == "Info"},
                        new SideChildMenu{ Name="我的建议", Url = "/Info/Suggest", IsActive = currentAction == "Suggest" && currentController == "Info"},
                        new SideChildMenu{ Name="行业信息", Url = "/Info/Industry", IsActive = currentAction == "Industry" && currentController == "Info"}
                    }}
            };

            //添加我的网点，根据权限
            var menus = AuthService.GetMenusByUserName(User.Identity.Name);
            var mySiteMenu = menus.Where(m => m.Id == 9).FirstOrDefault();
            if (mySiteMenu != null)
            {
                var siteMenu = new SideMenu { Name = "我的网点", Url = "/Site/Index", IconClass = "icon-screenshot", IsActive = currentController == "Site" && currentAction == "Index" };
                var sideChildMenus = new List<SideChildMenu>();
                var subSiteMenus = menus.Where(m => m.ParentId == 0);
                foreach (var subSite in subSiteMenus)
                {
                    var sideMenu = new SideChildMenu { Name = subSite.Name, Url = subSite.Url, IsActive = currentController == "Site" && subSite.Url.IndexOf(currentAction) > "Site".Length };
                    sideChildMenus.Add(sideMenu);
                }
                siteMenu.ChildMenus = sideChildMenus;

                sideMenus.Add(siteMenu);
            }

            return sideMenus;
        }

        private ShowNameViewModel CreateShowName(ActionExecutedContext filterContext)
        {
            var showName = new ShowNameViewModel();
            var controllerAttr = filterContext.Controller.GetType()
                .GetCustomAttributes(typeof(ShowNameAttribute), false)
                .FirstOrDefault() as ShowNameAttribute;
            if (controllerAttr != null)
            {
                if (!string.IsNullOrEmpty(controllerAttr.ControllerShowName))
                {
                    showName.ControllerShowName = controllerAttr.ControllerShowName;
                    showName.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                }
                if (!string.IsNullOrEmpty(controllerAttr.DefaultAction))
                {
                    showName.ActionName = controllerAttr.DefaultAction;
                }
                else
                {
                    showName.ActionName = "Index";
                }
            }

            var actionAttr = filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(ShowNameAttribute), false)
                .FirstOrDefault() as ShowNameAttribute;
            if (actionAttr != null)
            {
                if (!string.IsNullOrEmpty(actionAttr.ControllerShowName))
                {
                    showName.ControllerShowName = actionAttr.ControllerShowName;
                    showName.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                }
                if (!string.IsNullOrEmpty(actionAttr.ActionShowName))
                {
                    showName.ActionShowName = actionAttr.ActionShowName;
                    showName.ActionName = filterContext.ActionDescriptor.ActionName;
                }
            }

            return showName;
        }
        #endregion
    }
}