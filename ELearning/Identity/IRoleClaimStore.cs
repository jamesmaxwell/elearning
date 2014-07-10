using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ELearning.Identity
{
    /// <summary>
    /// 角色授权存储接口
    /// </summary>
    /// <typeparam name="TRole">角色接口</typeparam>
    public interface IRoleClaimStore<TRole> : IDisposable where TRole : class, Microsoft.AspNet.Identity.IRole
    {
        Task AddClaimAsync(TRole role, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(TRole role);
        Task RemoveClaimAsync(TRole role, Claim claim);
    }
}