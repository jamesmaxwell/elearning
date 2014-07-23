using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Attributes
{
    /// <summary>
    /// 角色授权声明自定义属性
    /// </summary>
    public class ClaimItemAttribute : Attribute
    {
        public ClaimItemAttribute() { }

        /// <summary>
        /// 声明名称，默认为Action名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 声明组，默认为Controller名称
        /// </summary>
        public string Group { get; set; }
    }

    /// <summary>
    /// 要忽略的声明项
    /// </summary>
    public class IgnorClainItemAttribute : Attribute
    {
    }
}