using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace Icode.Blog.Categories
{
    /// <summary>
    /// 定义分类数据库仓储的操作
    /// </summary>
    public interface ICategoryRepository : IRepository<Category,Guid>
    {
        /// <summary>
        /// 分页获取分类数据
        /// </summary>
        /// <param name="skipCount">跳过多少条</param>
        /// <param name="maxResultCount">获取多少条</param>
        /// <param name="taxonomy">分类方法的类型枚举</param>
        /// <param name="orderKeySelector">排序表达式</param>
        /// <param name="isDesc">是否倒序排序。默认true</param>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetEnumerableByPage<T>(int skipCount, int maxResultCount,
            TaxonomyEnum taxonomy,Expression<Func<Category, T>> orderKeySelector, bool isDesc = true);

        /// <summary>
        /// 校验分类是否已经存在
        /// </summary>
        /// <param name="categoryName">分类名称</param>
        /// <param name="parentId">父Id</param>
        /// <param name="taxonomy">分类枚举</param>
        /// <returns>true已经存在；false不存在</returns>
        Task<bool> CheckCategoryIsExist(string categoryName, Guid? parentId, TaxonomyEnum taxonomy);

    }
}