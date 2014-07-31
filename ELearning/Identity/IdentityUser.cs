using Microsoft.AspNet.Identity;
using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface 
    /// </summary>
    [Alias("EL_Users")]
    //默认密码 abc123
    [PostCreateTable("insert into el_users(id,passwordhash,username) values('u1','AHdogl4Q4+zlahVEUwc0fCNraypE95RTgnKikiOF4Ga+4jUPOakP6PFaSpO+VGli1Q==','admin');")]
    public class IdentityUser : IUser
    {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Constructor that takes user name as argument
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        /// <summary>
        /// User ID
        /// </summary>
        [PrimaryKey]
        [CustomField("NVARCHAR(128)")]
        public string Id { get; set; }

        /// <summary>
        /// User's name
        /// </summary>
        [CustomField("NVARCHAR(256)")]
        [Index]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User's password hash
        /// </summary>
        [CustomField("NVARCHAR(500)")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// User's security stamp
        /// </summary>
        [CustomField("NVARCHAR(500)")]
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [CustomField("NVARCHAR(256)")]
        public string Email { get; set; }
    }
}
