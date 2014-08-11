using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Common
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}