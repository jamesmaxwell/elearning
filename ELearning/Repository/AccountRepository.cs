using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ELearning.Models;
using ELearning.Identity;
using ELearning.ViewModels;
using ServiceStack.OrmLite;

namespace ELearning.Repository
{
    public interface IAccountRepository
    {
        /// <summary>
        /// 分页查询所有用户信息
        /// </summary>
        /// <param name="queryParam">分页查询信息</param>
        /// <returns>返回某页用户以及用户总数</returns>
        Tuple<List<IdentityUser>, long> GetUsersByParam(QueryParam queryParam);
    }

    public class AccountRepository : Repository, IAccountRepository
    {
        public Tuple<List<IdentityUser>, long> GetUsersByParam(QueryParam queryParam)
        {
            using (var db = ConnFactory.Open())
            {
                var builder = db.From<IdentityUser>();
                if (!string.IsNullOrEmpty(queryParam.SortName))
                {
                    if (queryParam.Order == "desc")
                        builder.OrderByFieldsDescending(queryParam.SortName);
                    else
                        builder.OrderByFields(queryParam.SortName);
                }

                if (!string.IsNullOrEmpty(queryParam.Search))
                    builder.Where(x => x.RealName.Contains(queryParam.Search));

                builder.Limit(queryParam.Offset, queryParam.Limit);

                var users = db.Select<IdentityUser>(builder);
                var userCount = db.Count<IdentityUser>(builder);

                return new Tuple<List<IdentityUser>, long>(users, userCount);
            }
        }
    }
}