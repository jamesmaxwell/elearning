﻿using Microsoft.AspNet.Identity;
using System;
using ServiceStack.DataAnnotations;

namespace ELearning.Identity
{
    /// <summary>
    /// Class that implements the ASP.NET Identity
    /// IUser interface 
    /// </summary>
    [Alias("EL_Users")]
    public class IdentityUser : IUser
    {
        public const string UserAuthMenuKey = "UserAuthMenuKey_{0}";

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
        [CustomField("NVARCHAR(50)")]
        [Index(Unique = true)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 中文名字
        /// </summary>
        [Index]
        [Required]
        [CustomField("NVARCHAR(20)")]
        public string RealName { get; set; }

        /// <summary>
        /// 状态，-1表示阻止的，0表示有效的
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 所属机构代码
        /// </summary>
        [Required]
        public int BelongsTo { get; set; }

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
