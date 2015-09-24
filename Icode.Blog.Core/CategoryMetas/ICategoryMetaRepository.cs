using System;
using Abp.Domain.Repositories;

namespace Icode.Blog.CategoryMetas
{
    /// <summary>
    /// 定义分类拓展类数据库仓储的操作
    /// </summary>
    public interface ICategoryMetaRepository : IRepository<CategoryMeta,Guid>
    {
         
    }
}