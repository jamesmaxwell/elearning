using ServiceStack.DataAnnotations;

namespace ELearning.Models
{
    [Alias("EL_RolePrivileges")]
    public class RolePrivilege
    {
        public string RoleId { get; set; }

        public string PrivilegeId { get; set; }
    }
}