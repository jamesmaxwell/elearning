using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    /// <summary>
    /// 区域信息
    /// </summary>
    [Alias("EL_Areas")]
    public class AreaInfo
    {
        public int Id { get; set; }

        [Required]
        [CustomField("NVARCHAR(100)")]
        public string Name { get; set; }

        [CustomField("NVARCHAR(200)")]
        public string Description { get; set; }

        public int ParentId { get; set; }
    }
}