using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Icode.Blog.Categories;
using Icode.Blog.Posts;

namespace Icode.Blog.EntityFramework.Repositories
{
    /// <summary>
    /// 文章信息仓储
    /// </summary>
    public class PostRepository : BlogRepositoryBase<Post, Guid>, IPostRepository
    {
        public PostRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 分页获取文章数据
        /// </summary>
        /// <param name="skipCount">跳过多少条</param>
        /// <param name="maxResultCount">获取多少条</param>
        /// <param name="whereExpression">where条件</param>
        /// <param name="orderKeySelector">排序表达式</param>
        /// <param name="isDesc">是否倒序排序。默认true</param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetEnumerableByPage<T>(int skipCount, int maxResultCount, Expression<Func<Post, bool>> whereExpression, Expression<Func<Post, T>> orderKeySelector,
            bool isDesc = true)
        {
            var resultTemp = base.GetAll().Where(whereExpression);
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
        /// 文章列表
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
        public async Task<IEnumerable<Post>> GetEnumerableByPage<T,T2>(int skipCount, int maxResultCount, Expression<Func<Post, bool>> whereExpression, Expression<Func<Post, T>> orderKeySelector, Expression<Func<Post, T2>> orderKeySelector2, bool isDesc = true, bool isDesc2 = true)
        {
            var resultTemp = base.GetAll().Where(whereExpression);
            if(isDesc)
            {
                resultTemp = resultTemp.OrderByDescending(orderKeySelector);
            }
            else
            {
                resultTemp = resultTemp.OrderBy(orderKeySelector);
            }

            if(isDesc2)
            {
                resultTemp = (resultTemp as IOrderedQueryable<Post>).ThenByDescending(orderKeySelector2);
            }
            else
            {
                resultTemp = (resultTemp as IOrderedQueryable<Post>).OrderBy(orderKeySelector2);
            }

            return await resultTemp.Skip(skipCount).Take(maxResultCount).ToListAsync();
        }

        /// <summary>
        /// 获取前十条数据
        /// 默认数据10条
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetEnumerableTopTen(int topNum=10)
        {
            return await base.GetAll().Where(w => w.Status == PostState.Publish).OrderByDescending(o => o.CreationTime).Take(topNum).ToListAsync();
        }

        /// <summary>
        /// 根据where条件获取文章数据
        /// </summary>
        /// <param name="whereExpression">where条件</param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetPostByWhere(Expression<Func<Post, bool>> whereExpression)
        {
            return await base.GetAll().Where(whereExpression).ToListAsync();
        }
    }
}