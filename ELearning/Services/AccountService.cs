using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELearning.Models;
using ELearning.ViewModels;
using ELearning.Repository;

namespace ELearning.Services
{
    /// <summary>
    /// 用户，角色相关服务接口
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 分页查询所有用户信息
        /// </summary>
        /// <param name="queryParam">分页查询信息</param>
        /// <returns></returns>
        PagedViewModel<UserInfoViewModel> GetUsersByParam(QueryParam queryParam);
    }

    public class AccountService : ServiceBase, IAccountService
    {
        public IAccountRepository AccountRepository { get; set; }

        public PagedViewModel<UserInfoViewModel> GetUsersByParam(QueryParam queryParam)
        {
            var pagedUsers = AccountRepository.GetUsersByParam(queryParam);
            var userInfos = new List<UserInfoViewModel>((int)pagedUsers.Item2);
            foreach (var user in pagedUsers.Item1)
            {
                userInfos.Add(new UserInfoViewModel
                {
                    UserName = user.UserName,
                    RealName = user.RealName,
                    BelongsTo = user.BelongsTo.ToString(),
                    Status = user.Status,
                    LastVisit = DateTime.Now,
                    Roles = new List<string>() { "r1" }
                });
            }

            return new PagedViewModel<UserInfoViewModel> { ViewModels = userInfos, Total = pagedUsers.Item2 };
        }
    }
}