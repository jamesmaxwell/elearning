using ServiceStack.DataAnnotations;

using ELearning.Identity;

namespace ELearning.Models
{
    [Alias("EL_RolePrivileges")]
    public class RolePrivilege
    {
        [AutoIncrement]
        public int Id { get; set; }

        [Index(Unique = false)]
        [Required]
        [CustomField("NVARCHAR(128)")]
        [References(typeof(IdentityRole))]
        public string RoleId { get; set; }

        [Required]
        [CustomField("NVARCHAR(128)")]
        [References(typeof(Privilege))]
        public string PrivilegeId { get; set; }
    }
}