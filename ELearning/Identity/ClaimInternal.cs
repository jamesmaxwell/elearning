using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    /// <summary>
    /// 角色授权声明
    /// </summary>
    [Alias("EL_RoleClaims")]
    internal class ClaimInternal
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限声明，Controller+Actoin 的形式
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 是否有权限，1有权限，0无权限
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// 授权分级
        /// </summary>
        public string ClaimGroup { get; set; }

        /// <summary>
        /// 授权显示名称
        /// </summary>
        public string ClaimName { get; set; }
    }
}
