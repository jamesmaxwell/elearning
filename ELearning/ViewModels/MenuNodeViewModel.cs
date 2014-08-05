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
        public Node<Menu> TopRoot { get; set; }

        public Node<Menu> LeftRoot { get; set; }
    }
}