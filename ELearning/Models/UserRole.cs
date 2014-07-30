using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    [Alias("EL_UserRoles")]
    public class UserRole
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
}