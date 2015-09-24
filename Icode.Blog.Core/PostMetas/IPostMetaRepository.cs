using System;
using Abp.Domain.Repositories;

namespace Icode.Blog.PostMetas
{
    /// <summary>
    /// 定义文章拓展类数据库仓储操作
    /// </summary>
    public interface IPostMetaRepository : IRepository<PostMeta,Guid>
    {
         
    }
}