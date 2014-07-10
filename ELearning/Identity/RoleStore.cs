using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ELearning.Common;

namespace ELearning.Identity
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class RoleStore : IRoleStore<IdentityRole>, IRoleClaimStore<IdentityRole>
    {
        private RoleTable roleTable;
        private RoleClaimsTable roleClaimsTable;

        public MsSQLDatabase Database { get; private set; }

        /// <summary>
        /// Default constructor that initializes a new MySQLDatabase
        /// instance using the Default Connection string
        /// </summary>
        public RoleStore()
            : this(new MsSQLDatabase())
        {
        }

        /// <summary>
        /// Constructor that takes a MySQLDatabase as argument 
        /// </summary>
        /// <param name="database"></param>
        public RoleStore(MsSQLDatabase database)
        {
            Database = database;
            roleTable = new RoleTable(database);
            roleClaimsTable = new RoleClaimsTable(database);
        }

        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            roleTable.Delete(role.Id);

            return Task.FromResult<Object>(null);
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            IdentityRole result = roleTable.GetRoleById(roleId);

            return Task.FromResult<IdentityRole>(result);
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            IdentityRole result = roleTable.GetRoleByName(roleName);

            return Task.FromResult<IdentityRole>(result);
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            roleTable.Update(role);

            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            if (Database != null)
            {
                Database = null;
            }
        }

        /// <summary>
        /// Inserts a claim to the RoleClaimsTable for the given role
        /// </summary>
        /// <param name="role">Role to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public Task AddClaimAsync(IdentityRole role, Claim claim)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            roleClaimsTable.Insert(claim, role.Id);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns all claims for a given Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(IdentityRole role)
        {
            var claims = roleClaimsTable.FindByRoleId(role.Id);

            return Task.FromResult<IList<Claim>>(claims);
        }

        /// <summary>
        /// Removes a claim froma role
        /// </summary>
        /// <param name="role">Role to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(IdentityRole role, Claim claim)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            roleClaimsTable.Delete(role, claim);

            return Task.FromResult<object>(null);
        }
    }
}
