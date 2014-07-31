using ServiceStack.DataAnnotations;

using ELearning.Identity;

namespace ELearning.Models
{
    [Alias("EL_RolePrivileges")]
    [PostCreateTable(@"insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p1');
                       insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p2');
                       insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p3');
                       insert into EL_RolePrivileges(RoleId, PrivilegeId) values('r1','p4');")]
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