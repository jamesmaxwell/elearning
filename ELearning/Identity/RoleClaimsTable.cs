using System.Collections.Generic;
using System.Security.Claims;
using ServiceStack.OrmLite;
using ELearning.Common;

namespace ELearning.Identity
{
    /// <summary>
    /// Class that represents the RoleClaims table in the MySQL Database
    /// </summary>
    public class RoleClaimsTable
    {
        private MsSQLDatabase _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public RoleClaimsTable(MsSQLDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a roleId
        /// </summary>
        /// <param name="roleId">The role's id</param>
        /// <returns></returns>
        public IList<Claim> FindByRoleId(string roleId)
        {
            var claims = new List<Claim>();

            using (var db = _database.Open())
            {
                var claimInners = db.Select<ClaimInternal>(q => q.Where(x => x.RoleId == roleId));
                foreach (var claim in claimInners)
                {
                    claims.Add(new Claim(claim.ClaimType, claim.ClaimValue, "bool", claim.ClaimName, claim.ClaimGroup));
                }
            }
            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string roleId)
        {
            using (var db = _database.Open())
            {
                return db.Delete<ClaimInternal>(x => x.RoleId == roleId);
            }
        }

        /// <summary>
        /// Inserts a new claim in RoleClaims table
        /// </summary>
        /// <param name="claim">Role's claim to be added</param>
        /// <param name="roleId">Role's id</param>
        /// <returns></returns>
        public int Insert(Claim claim, string roleId)
        {
            using (var db = _database.Open())
            {
                return (int)db.Insert<ClaimInternal>(
                    new ClaimInternal
                    {
                        RoleId = roleId,
                        ClaimType = claim.Type,
                        ClaimValue = claim.Value,
                        ClaimName = claim.Issuer,
                        ClaimGroup = claim.OriginalIssuer
                    });
            }
        }

        /// <summary>
        /// Deletes a claim from a role 
        /// </summary>
        /// <param name="role">The role to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from role</param>
        /// <returns></returns>
        public int Delete(IdentityRole role, Claim claim)
        {
            using (var db = _database.Open())
            {
                return db.Delete<ClaimInternal>(q => q.Where(x =>
                    x.RoleId == role.Id &&
                    x.ClaimType == claim.Type));
            }
        }
    }
}
