using System;
using Abp.EntityFramework;
using Icode.Blog.CommentMetas;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 评论拓展类仓储
    /// </summary>
    public class CommentMetaRepository : BlogRepositoryBase<CommentMeta,Guid>,ICommentMetaRepository
    {
        public CommentMetaRepository(IDbContextProvider<BlogDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}