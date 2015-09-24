using System;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Icode.Blog.UserInfos;

namespace Icode.Blog.EntityFramework.Repositories
{
   /// <summary>
   /// 用户信息仓储
   /// </summary>
    public class UserInfoRepository : BlogRepositoryBase<UserInfo, long>,IUserInfoRepository
    {
        public UserInfoRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}