using System;
using Abp.EntityFramework;
using Icode.Blog.UserMetas;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 用户信息扩展类仓储
    /// </summary>
    public class UserMetaRepository : BlogRepositoryBase<UserMeta, Guid>,IUserMetaRepository
    {
        public UserMetaRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}