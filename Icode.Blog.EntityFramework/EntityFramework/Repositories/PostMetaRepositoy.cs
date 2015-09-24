using System;
using Abp.EntityFramework;
using Icode.Blog.PostMetas;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 文章拓展类仓储
    /// </summary>
    public class PostMetaRepositoy : BlogRepositoryBase<PostMeta,Guid>,IPostMetaRepository
    {
        public PostMetaRepositoy(IDbContextProvider<BlogDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}