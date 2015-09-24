using System;
using Abp.Domain.Repositories;

namespace Icode.Blog.Comments
{
    /// <summary>
    /// 定义评论数据库仓储的操作
    /// </summary>
    public interface ICommentRepository : IRepository<Comment,Guid>
    {
         
    }
}