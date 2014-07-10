using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    [Alias("EL_UserLogins")]
    internal class UserLoginInternal
    {
        public string UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
