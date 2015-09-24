using System.Threading.Tasks;
using Abp.Application.Services;
using Icode.Blog.Application.Dto;
using Icode.Blog.Posts.Dtos;
using System;
using Icode.Blog.Categories.Dtos;

namespace Icode.Blog.Posts
{
    public interface IPostService : IApplicationService
    {
        /// <summary>
        /// 分页获取文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPostListOutput> GetPostsByPage(GetPostListInput input);

        /// <summary>
        /// 前端获取文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetPostListOutput> PostList(GetPostListInput input);

        /// <summary>
        /// 添加或修改文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RenderMessageOutput> CreateOrUpdatePost(CreatePostInput input);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePost(DeleteInput<Guid> input);

        /// <summary>
        /// 获取前十篇文章
        /// </summary>
        /// <returns></returns>
        Task<GetTopTenOutput> GetTopTen();

        /// <summary>
        /// 根据文章Id或文章友好地址获取文章
        /// </summary>
        /// <param name="idOrentryName">文章的Id或者友好地址</param>
        /// <returns></returns>
        Task<GetPostOutput> GetPostByIdOrEntryName(GetPostInput input);

        /// <summary>
        /// 获取文章状态
        /// </summary>
        /// <returns></returns>
        Task<EnumOutput> GetPostState();

        /// <summary>
        /// 获取评论设置状态枚举
        /// </summary>
        /// <returns></returns>
        Task<EnumOutput> GetCommonSettingState();

        /// <summary>
        /// 根据PostId获取分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetCategoryOutput> GetCategoriesByPostId(GetPostInput input);

    }
}