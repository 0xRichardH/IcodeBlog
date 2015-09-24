using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Icode.Blog.Categories;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 分类信息仓储
    /// </summary>
    public class CategoryRepository : BlogRepositoryBase<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 分页获取分类数据
        /// </summary>
        /// <param name="skipCount">跳过多少条</param>
        /// <param name="maxResultCount">获取多少条</param>
        /// <param name="taxonomy">分类方法的类型枚举</param>
        /// <param name="orderKeySelector">排序表达式</param>
        /// <param name="isDesc">是否倒序排序。默认true</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Category>> GetEnumerableByPage<T>(int skipCount, int maxResultCount,
            TaxonomyEnum taxonomy, Expression<Func<Category, T>> orderKeySelector, bool isDesc = true)
        {
            var resultTemp = base.GetAll().Where(w => w.Taxonomy == taxonomy);
            if (isDesc)
            {
                return await resultTemp.OrderByDescending(orderKeySelector).Skip(skipCount).Take(maxResultCount).ToListAsync();
            }
            else
            {
                return await resultTemp.OrderBy(orderKeySelector).Skip(skipCount).Take(maxResultCount).ToListAsync();
            }
        }

        /// <summary>
        /// 校验分类是否已经存在
        /// </summary>
        /// <param name="categoryName">分类名称</param>
        /// <param name="parentId">父Id</param>
        /// <param name="taxonomy">分类枚举</param>
        /// <returns>true已经存在；false不存在</returns>
        public virtual async Task<bool> CheckCategoryIsExist(string categoryName, Guid? parentId, TaxonomyEnum taxonomy)
        {
            int count = await base.CountAsync(w => w.CategoryName == categoryName &&
                           w.ParentId == parentId &&
                           w.Taxonomy == taxonomy);
            return count > 0;

        }
    }
}