using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.ViewModels
{
    /// <summary>
    /// 分页查询参数
    /// </summary>
    public class QueryParam
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string Search { get; set; }

        public string SortName { get; set; }

        public string Order { get; set; }
    }
}