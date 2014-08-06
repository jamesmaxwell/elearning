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
        /// <summary>
        /// 顶部菜单
        /// </summary>
        public Node<Menu> TopRoot { get; set; }

        /// <summary>
        /// 我的网点
        /// </summary>
        public Node<Menu> MySiteNode { get; set; }
    }
}