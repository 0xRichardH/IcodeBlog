using System;
using Abp.Domain.Repositories;
using Icode.Blog.Comments;

namespace Icode.Blog.CommentMetas
{
    /// <summary>
    /// 定义评论拓展类数据库仓储的操作
    /// </summary>
    public interface ICommentMetaRepository : IRepository<CommentMeta,Guid>
    {
         
    }
}