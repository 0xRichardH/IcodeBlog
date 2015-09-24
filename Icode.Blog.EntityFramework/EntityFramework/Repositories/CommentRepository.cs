using System;
using Abp.EntityFramework;
using Icode.Blog.Comments;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 评论信息
    /// </summary>
    public class CommentRepository : BlogRepositoryBase<Comment,Guid>,ICommentRepository
    {
        public CommentRepository(IDbContextProvider<BlogDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}