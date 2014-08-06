using System;

namespace ELearning.Attributes
{
    public class ShowNameAttribute : Attribute
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }
    }
}