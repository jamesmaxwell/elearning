using System;

namespace ELearning.Attributes
{
    public class ShowNameAttribute : Attribute
    {
        /// <summary>
        /// 控制器默认Action
        /// </summary>
        public string DefaultAction { get; set; }

        /// <summary>
        /// Action中文名称
        /// </summary>
        public string ActionShowName { get; set; }

        /// <summary>
        /// 控制器中文名称
        /// </summary>
        public string ControllerShowName { get; set; }
    }
}