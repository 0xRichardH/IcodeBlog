using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Icode.Blog.Categories;

namespace Icode.Blog.Posts
{
    /// <summary>
    /// 定义文章数据库操作
    /// </summary>
    public interface IPostRepository : IRepository<Post,Guid>
    {
        /// <summary>
        /// 分页获取文章数据
        /// </summary>
        /// <param name="skipCount">跳过多少条</param>
        /// <param name="maxResultCount">获取多少条</param>
        /// <param name="whereExpression">where条件</param>
        /// <param name="orderKeySelector">排序表达式</param>
        /// <param name="isDesc">是否倒序排序。默认true</param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetEnumerableByPage<T>(int skipCount, int maxResultCount,
            Expression<Func<Post, bool>> whereExpression, Expression<Func<Post,T>> orderKeySelector,
            bool isDesc = true);

        /// <summary>
        /// 分页获取文章
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="whereExpression"></param>
        /// <param name="orderKeySelector"></param>
        /// <param name="orderKeySelector2"></param>
        /// <param name="isDesc"></param>
        /// <param name="isDesc2"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetEnumerableByPage<T,T2>(int skipCount, int maxResultCount,
           Expression<Func<Post, bool>> whereExpression, Expression<Func<Post, T>> orderKeySelector,
           Expression<Func<Post, T2>> orderKeySelector2,bool isDesc=true,bool isDesc2=true);

        /// <summary>
        /// 获取前十条数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetEnumerableTopTen(int topNum=10);

        /// <summary>
        /// 根据条件获取文章
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetPostByWhere(Expression<Func<Post, bool>> whereExpression);
    }
}