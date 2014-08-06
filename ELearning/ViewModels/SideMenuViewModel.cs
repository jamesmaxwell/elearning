using System.Collections.Generic;

namespace ELearning.ViewModels
{
    public class SideMenuViewModel
    {
        public List<SideMenu> SideMenus { get; set; }
    }

    public class SideMenu
    {
        private List<SideChildMenu> _childMenus = new List<SideChildMenu>();
        public string IconClass { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public List<SideChildMenu> ChildMenus { get { return _childMenus; } set { _childMenus = value; } }
    }

    public class SideChildMenu
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}