using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using AutoMapper;
using Icode.Blog.Application.Dto;
using Icode.Blog.Posts.Dtos;
using Icode.Blog.Categories;
using Abp.AutoMapper;
using System.Web;
using System.Linq;
using Abp.Extensions;
using Abp.Runtime.Caching;
using Abp.Encrypt;
using System.Runtime.Caching;
using Abp.Domain.Uow;
using Icode.Blog.Categories.Dtos;
using Abp.Authorization;

namespace Icode.Blog.Posts
{
    //[AbpAuthorize]
    public class PostService : ApplicationService, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            this._postRepository = postRepository;
            this._categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 分页获取文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork(isTransactional: false)]
        public async Task<GetPostListOutput> GetPostsByPage(GetPostListInput input)
        {
            //跳过条数
            var skipCount = (input.PageIndex - 1) * input.PageSize;
            //获取条数
            var maxResultCount = input.PageSize;

            //获取总条数
            var totalCount = await _postRepository.CountAsync();
            //获取当前页的数据
            var items = await this._postRepository.GetEnumerableByPage(skipCount, maxResultCount, w => true, o => o.CreationTime);

            //稍微处理一下结果集
            items = items.Select(w => new Post
            {
                Id = w.Id,
                Title = w.Title,
                EntryName = w.EntryName,
                Status = w.Status,
                PVCount = w.PVCount
            });

            return  new GetPostListOutput
            {
                Items = Mapper.Map<IReadOnlyList<PostDto>>(items),
                TotalCount = totalCount,
                PageIndex = input.PageIndex
    };
}

/// <summary>
/// 前端获取文章
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
[UnitOfWork(isTransactional: false)]
public async Task<GetPostListOutput> PostList(GetPostListInput input)
{
    //跳过条数
    var skipCount = (input.PageIndex - 1) * input.PageSize;
    //获取条数
    var maxResultCount = input.PageSize;

    //获取总条数
    var totalCount = await _postRepository.CountAsync();
    //获取当前页的数据
    var items = await this._postRepository.GetEnumerableByPage(skipCount, maxResultCount, w => w.Status == PostState.Publish, o => o.IsTop, o => o.CreationTime);

    return new GetPostListOutput
    {
        Items = Mapper.Map<IReadOnlyList<PostDto>>(items),
        TotalCount = totalCount,
        PageIndex = input.PageIndex
    };
}

/// <summary>
/// 添加文章或修改文章
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
[AbpAuthorize]
public async Task<RenderMessageOutput> CreateOrUpdatePost(CreatePostInput input)
{
    //判断友好地址是否已经存在
    if (!input.EntryName.IsNullOrEmpty())
    {
        var getPost = (await this._postRepository.GetPostByWhere(w => w.EntryName.ToLower() == input.EntryName.ToLower())).FirstOrDefault();
        bool isExistEntryName = getPost != null;
        if (isExistEntryName && getPost.Id != input.Id)
        {
            return new RenderMessageOutput
            {
                Success = false,
                Message = "友好地址已经存在"
            };
        }
    }

    Logger.Info("Creating post for input : " + input);

    Post post;
    if (input.Id.HasValue)
    {
        post = await this._postRepository.GetAsync((Guid)(input.Id));
    }
    else
    {
        post = new Post();
        post.Id = Guid.NewGuid();
    }


    post.Title = input.Title;
    post.Content = input.Content;
    post.Status = input.Status;
    post.CommentStatus = input.CommentStatus;
    post.EntryName = input.EntryName;
    post.IsShowRSS = input.IsShowRSS;
    post.IsOriginal = input.IsOriginal;
    post.IsTop = input.IsTop;
    post.LastModificationTime = DateTime.Now;//文章列表排序需要

    if (!input.Password.IsNullOrEmpty())
    {
        //加密解密
        post.Password = EncryptHelper.EncryptStr(input.Password);
    }

    //摘要
    if (input.Except.IsNullOrEmpty())
    {
        var except = input.Content.TrimHtml();
        post.Except = except.Length > 250 ? except.Substring(0, 250) + "..." : except;
    }
    else
    {
        post.Except = input.Except.TrimHtml();
    }

    //创建者或修改者
    if (input.UserId.HasValue)
    {
        post.UserId = input.UserId;//TODO:登录后为登录用户信息
    }

    if (input.CategoryArray != null && input.CategoryArray.Length > 0)
    {
        if (post.Categories == null)
        {
            post.Categories = new List<Category>();
        }
        else
        {
            foreach (Category category in post.Categories)
            {
                if (category.Count > 0)
                {
                    category.Count--;
                }
            }
        }
        post.Categories.Clear();
        foreach (var categoryId in input.CategoryArray)
        {
            var category = await this._categoryRepository.GetAsync(categoryId);
            category.Count++;//增加当前分类的数量
            post.Categories.Add(category);
        }
    }

    //根据编辑和新增不同
    if (!input.Id.HasValue)
    {
        await this._postRepository.InsertAsync(post);
    }
    return new RenderMessageOutput();
}

/// <summary>
/// 删除文章
/// </summary>
/// <param name="Id"></param>
/// <returns></returns>
[AbpAuthorize]
public async Task DeletePost(DeleteInput<Guid> input)
{
    Logger.Info("Deleting post for Id : " + input);
    await this._postRepository.DeleteAsync(input.Id);
}

[UnitOfWork(isTransactional: false)]
/// <summary>
/// 获取前10篇文章
/// </summary>
/// <returns></returns>
public async Task<GetTopTenOutput> GetTopTen()
{
    //从缓存获取数据
    const string cacheKey = "GetTopTen";

    AsyncThreadSafeObjectCache<GetTopTenOutput> cache = new AsyncThreadSafeObjectCache<GetTopTenOutput>(MemoryCache.Default, TimeSpan.FromSeconds(120));
    GetTopTenOutput output = await cache.GetAsync(cacheKey, async () =>
   {
               //获取前10条数据
               var posts = await this._postRepository.GetEnumerableTopTen(10);

       output = new GetTopTenOutput
       {
           Items = posts.MapTo<IReadOnlyList<TopTenPostDto>>()
       };
       cache.Set(cacheKey, output);
       return output;
   });

    if (output == null)//缓存不存在数据
    {

    }

    return output;
}

/// <summary>
/// 根据文章Id或文章友好地址获取文章
/// </summary>
/// <param name="idOrentryName">文章的Id或者友好地址</param>
/// <returns></returns>
public async Task<GetPostOutput> GetPostByIdOrEntryName(GetPostInput input)
{
    string idOrentryName = input.IdOrEntryName;
    if (idOrentryName.IsNullOrEmpty()) throw new HttpException(404, "Not Found");

    Guid guidId;
    Post post = null;
    if (Guid.TryParse(idOrentryName, out guidId))
    {
        //Id
        var postIEnumable = await this._postRepository.GetPostByWhere(w => w.Id == guidId);
        post = postIEnumable.FirstOrDefault();
    }
    else
    {
        //EntryName
        var postIEnumable = await this._postRepository.GetPostByWhere(w => w.EntryName.ToLower() == idOrentryName.ToLower());
        post = postIEnumable.FirstOrDefault();
    }

    //校验获取的数据
    if (post == null) throw new HttpException(404, "Not Found");

    post.PVCount++;//TODOL:文章的访问量。目前是自加1 以后根据用户ip计算

    return new GetPostOutput
    {
        Post = post.MapTo<PostDto>()
    };
}

/// <summary>
/// 获取文章状态的集合
/// </summary>
/// <returns></returns>
public async Task<EnumOutput> GetPostState()
{
    var enumDict = await typeof(PostState).EnumToDictionary();


    return new EnumOutput
    {
        Dict = enumDict,
        Values = enumDict.Keys.ToArray()
    };
}

/// <summary>
/// 获取评论设置状态枚举
/// </summary>
/// <returns></returns>
public async Task<EnumOutput> GetCommonSettingState()
{
    var enumDict = await typeof(CommentSettingState).EnumToDictionary();
    return new EnumOutput
    {
        Dict = enumDict,
        Values = enumDict.Keys.ToArray()
    };
}

/// <summary>
/// 根据文章Id获取所有的分类（包括标签）
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
[UnitOfWork(isTransactional: false)]
public async Task<GetCategoryOutput> GetCategoriesByPostId(GetPostInput input)
{
    Guid postId = input.IdOrEntryName.ToGuid();

    var post = (await this._postRepository.GetPostByWhere(w => w.Id == postId)).FirstOrDefault();

    var categories = post.Categories;

    return new GetCategoryOutput
    {
        CategoryCollection = categories.MapTo<IReadOnlyList<CategoryDto>>()
    };
}

    }
}