using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    [Alias("EL_UserLogins")]
    public class UserLoginInternal
    {
        [Required]
        [References(typeof(IdentityUser))]
        [CustomField("NVARCHAR(128)")]
        public string UserId { get; set; }

        [Required]
        [CustomField("NVARCHAR(128)")]
        public string LoginProvider { get; set; }

        [Required]
        [CustomField("NVARCHAR(128)")]
        public string ProviderKey { get; set; }
    }
}
