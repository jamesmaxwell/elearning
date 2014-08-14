using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    /// <summary>
    /// 用户岗位
    /// </summary>
    [Alias("EL_UserPosition")]
    public class UserPosition
    {
        /// <summary>
        /// 岗位编号
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [CustomField("NVARCHAR(50)")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Weight { get; set; }
    }
}