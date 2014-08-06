using System;

namespace ELearning.Attributes
{
    public class ShowNameAttribute : Attribute
    {
        /// <summary>
        /// 控制器默认Action
        /// </summary>
        public string DefaultAction { get; set; }

        public string ActionShowName { get; set; }

        public string ControllerShowName { get; set; }
    }
}