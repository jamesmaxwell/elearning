using Microsoft.AspNet.Identity;
using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IRole interface 
    /// </summary>
    [Alias("EL_Roles")]
    [PostCreateTable("insert into el_roles(id,name) values('r1','管理员');" +
                    "insert into el_roles(id,name) values('r2','普通用户');")]
    public class IdentityRole : IRole
    {
        /// <summary>
        /// Default constructor for Role 
        /// </summary>
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// Constructor that takes names as argument 
        /// </summary>
        /// <param name="name"></param>
        public IdentityRole(string name)
            : this()
        {
            Name = name;
        }

        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }

        /// <summary>
        /// Role ID
        /// </summary>
        [PrimaryKey]
        [CustomField("NVARCHAR(128)")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [CustomField("NVARCHAR(128)")]
        [Required]
        public string Name { get; set; }
    }
}
