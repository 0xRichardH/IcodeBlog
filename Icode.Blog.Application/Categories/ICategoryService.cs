using System.Threading.Tasks;
using Abp.Application.Services;
using Icode.Blog.Application.Dto;
using Icode.Blog.Categories.Dtos;
using System;

namespace Icode.Blog.Categories
{

    public interface ICategoryService : IApplicationService
    {
        /// <summary>
        /// 根据分类方法枚举获取分类
        /// </summary>
        /// <param name="taxonomy"></param>
        /// <returns></returns>
        Task<GetCategoryOutput> GetAllByTaxonomy(TaxonomyEnum taxonomy);

        /// <summary>
        /// 获取所有的标签
        /// </summary>
        /// <returns></returns>
        Task<GetCategoryOutput> GetAllTag();

        /// <summary>
        /// 获取所有的分类
        /// </summary>
        /// <returns></returns>
        Task<GetCategoryOutput> GetAllCategory();

        /// <summary>
        /// 分页获取分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CategoryPageOutput> GetCategoryByPage(CategoryPageInput input);

        /// <summary>
        /// 分页获取标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CategoryPageOutput> GetTagByPage(CategoryPageInput input);

        /// <summary>
        /// 分页获取分类和标签
        /// </summary>
        /// <param name="input"></param>
        /// <param name="taxonomy"></param>
        /// <returns></returns>
        Task<CategoryPageOutput> GetByPage(CategoryPageInput input, TaxonomyEnum taxonomy);

        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RenderMessageOutput> CreateCategory(CreateCategoryInput input);

        /// <summary>
        /// 修改文章分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RenderMessageOutput> UpdateCategory(UpdateCategoryInput input);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCategory(DeleteInput<Guid> input);

        /// <summary>
        /// 获取一条分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CategoryOutput> GetCategory(GetCategoryInput input);
    }
}