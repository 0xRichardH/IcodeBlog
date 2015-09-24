using System;
using Abp.EntityFramework;
using Icode.Blog.CategoryMetas;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 分类拓展信息仓储
    /// </summary>
    public class CategoryMetaRepository : BlogRepositoryBase<CategoryMeta,Guid>,ICategoryMetaRepository
    {
        public CategoryMetaRepository(IDbContextProvider<BlogDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}