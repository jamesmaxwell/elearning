using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELearning.Common;
using ELearning.Models;

namespace ELearning.ViewModels
{
    public class MenuNodeViewModel
    {
        public Node<Menu> TopMenu { get; set; }

        public Node<Menu> LeftMenu { get; set; }
    }
}