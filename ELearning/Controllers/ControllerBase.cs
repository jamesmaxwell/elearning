using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.Mvc.MiniProfiler;
using ELearning.Models;
using ELearning.ViewModels;
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
            var leftRoots = menus.Where(m => m.Position == MenuPosition.Left && m.ParentId == 0).ToList();

            var menuVM = new MenuNodeViewModel();
            var topRoot = new Node<Menu>(new Menu { Id = -1 });
            var leftRoot = new Node<Menu>(new Menu { Id = -2 });

            CreateMenuNode(topRoot, topRoots, menus);
            CreateMenuNode(leftRoot, leftRoots, menus);

            menuVM.TopRoot = topRoot;
            menuVM.LeftRoot = leftRoot;

            ViewBag.MenuViewModel = menuVM;
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