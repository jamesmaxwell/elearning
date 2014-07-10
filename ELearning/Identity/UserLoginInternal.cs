using ServiceStack.DataAnnotations;

namespace AspNet.Identity.ServiceStack
{
    [Alias("EL_UserLogins")]
    internal class UserLoginInternal
    {
        public string UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
