using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Alias("EL_Department")]
    public class Department
    {
        public int Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(100)")]
        public string Name { get; set; }

        /// <summary>
        /// 部门描述
        /// </summary>
        [CustomField("NVARCHAR(200)")]
        public string Description { get; set; }

        /// <summary>
        /// 父部门id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Weight { get; set; }
    }
}