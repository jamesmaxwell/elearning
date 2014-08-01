using ServiceStack.DataAnnotations;
using ELearning.Identity;

namespace ELearning.Models
{
    [Alias("EL_UserRoles")]
    public class UserRole
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [CustomField("NVARCHAR(128)")]
        [Required]
        [References(typeof(IdentityUser))]
        [Index(Unique = false)]
        public string UserId { get; set; }

        [Required]
        [CustomField("NVARCHAR(128)")]
        [References(typeof(IdentityRole))]
        public string RoleId { get; set; }
    }
}