﻿using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    /// <summary>
    /// 系统操作权限实体
    /// </summary>
    [Alias("EL_Privileges")]
    public class Privilege
    {
        /// <summary>
        /// 所有授权项的缓存key
        /// </summary>
        public const string AllPrivilegeCacheKey = "AllPrivilegeCacheKey";

        [PrimaryKey]
        [CustomField("NVARCHAR(128)")]
        public String Id { get; set; }

        /// <summary>
        /// 权限声明，Controller.类型 的形式，类型预设为'view','admin'两种。
        /// view代表查看权限，对应Index和Details默认Action
        /// admin代表管理权限，对应Create,Edit,Delete默认Action
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(200)")]
        public string PrivilegeType { get; set; }

        /// <summary>
        /// 是否有权限，1有权限，0无权限
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(100)")]
        public string PrivilegeValue { get; set; }

        /// <summary>
        /// 授权显示名称
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(200)")]
        public string PrivilegeName { get; set; }

        /// <summary>
        /// 授权分组代码
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(200)")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 授权分组名称
        /// </summary>
        [Required]
        [CustomField("NVARCHAR(200)")]
        public string GroupName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required]
        public int PrivilegeIndex { get; set; }
    }
}
