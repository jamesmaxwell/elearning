using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.ViewModels
{
    public class PagedViewModel<T> where T : class ,new()
    {
        public long Total { get; set; }

        public List<T> ViewModels { get; set; }
    }
}