using System.Collections.Generic;
using ServiceStack.OrmLite;
using ELearning.Common;

namespace AspNet.Identity.ServiceStack
{
    /// <summary>
    /// Class that represents the UserRoles table in the MySQL Database
    /// </summary>
    public class UserRolesTable
    {
        private MsSQLDatabase _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesTable(MsSQLDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            string commandText = @"Select EL_Roles.Name from EL_UserRoles, EL_Roles 
                                  where EL_UserRoles.UserId = @userId and
                                        EL_UserRoles.RoleId = EL_Roles.Id";
            using (var db = _database.Open())
            {
                return db.Select<string>(commandText, new { userId = userId });
            }
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            var commandText = "delete EL_UserRoles where UserId = @userId";
            using (var db = _database.Open())
            {
                return db.ExecuteNonQuery(commandText, new { userId = userId });
            }
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, string roleId)
        {
            string commandText = "Insert into EL_UserRoles (UserId, RoleId) values (@userId, @roleId)";
            using (var db = _database.Open())
            {
                return db.ExecuteNonQuery(commandText, new { userId = user.Id, roleId = roleId });
            }
        }
    }
}
