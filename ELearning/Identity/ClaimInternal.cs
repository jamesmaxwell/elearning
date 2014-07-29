using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    /// <summary>
    /// 角色授权声明
    /// </summary>
    [Alias("EL_Claims")]
    internal class ClaimInternal
    {
        [PrimaryKey]
        public String Id { get; set; }

        /// <summary>
        /// 权限声明，Controller.类型 的形式，类型预设为'view','admin'两种。
        /// view代表查看权限，对应Index和Details默认Action
        /// admin代表管理权限，对应Create,Edit,Delete默认Action
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 是否有权限，1有权限，0无权限
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// 授权分组代码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 授权分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 授权显示名称
        /// </summary>
        public string ClaimName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int ClaimIndex { get; set; }
    }
}
