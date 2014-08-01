using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ELearning.Identity;

namespace ELearning.Models
{
    /// <summary>
    /// 角色，菜单对应实体
    /// </summary>
    [Alias("EL_RoleMenus")]
    public class RoleMenu
    {
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(128)")]
        [ForeignKey(typeof(IdentityRole), OnDelete = "CASCADE")]
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        [ForeignKey(typeof(Menu), OnDelete = "CASCADE")]
        public int MenuId { get; set; }
    }
}