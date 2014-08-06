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

            var menus = AuthService.GetMenusByUserName(User.Identity.Name);
            var topRoots = menus.Where(m => m.Position == MenuPosition.Top && m.ParentId == 0).ToList();

            var menuVM = new MenuNodeViewModel();
            var topRoot = new Node<Menu>(new Menu { Id = -1 });

            CreateMenuNode(topRoot, topRoots, menus);
            menuVM.TopRoot = topRoot;

            //我的网点
            var mySiteMenu = menus.Where(m => m.Id == 9).FirstOrDefault();
            if (mySiteMenu != null)
            {
                var mySiteNode = new Node<Menu>(mySiteMenu);
                var siteSubMenus = menus.Where(m => m.ParentId == 9);
                foreach (var subMenu in siteSubMenus)
                {
                    mySiteNode.Add(subMenu);
                }

                menuVM.MySiteNode = mySiteNode;
            }

            ViewBag.MenuViewModel = menuVM;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var showName = new ShowNameViewModel();
            var controllerAttr = filterContext.Controller.GetType()
                .GetCustomAttributes(typeof(ShowNameAttribute), false)
                .FirstOrDefault() as ShowNameAttribute;
            if (controllerAttr != null && !string.IsNullOrEmpty(controllerAttr.ControllerName))
            {
                showName.ControllerShowName = controllerAttr.ControllerName;
                showName.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }

            var actionAttr = filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(ShowNameAttribute), false)
                .FirstOrDefault() as ShowNameAttribute;
            if (actionAttr != null)
            {
                if (!string.IsNullOrEmpty(actionAttr.ControllerName))
                {
                    showName.ControllerShowName = actionAttr.ControllerName;
                    showName.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                }
                if (!string.IsNullOrEmpty(actionAttr.ActionName))
                {
                    showName.ActionShowName = actionAttr.ActionName;
                    showName.ActionName = filterContext.ActionDescriptor.ActionName;
                }
            }

            ViewBag.ShowName = showName;

            base.OnActionExecuted(filterContext);
        }

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
    }
}