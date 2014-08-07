using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.ViewModels
{
    public class UserInfoViewModel
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public int Status { get; set; }

        public string BelongsTo { get; set; }

        public List<string> Roles { get; set; }

        public DateTime LastVisit { get; set; }
    }
}