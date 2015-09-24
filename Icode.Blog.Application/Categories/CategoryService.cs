using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using AutoMapper;
using Castle.Core.Internal;
using Icode.Blog.Application.Dto;
using Icode.Blog.Categories.Dtos;
using Abp.Runtime.Caching;
using Abp.Extensions;
using Abp.Domain.Uow;
using Abp.Authorization;
namespace Icode.Blog.Categories
{
    //[AbpAuthorize]
    public class CategoryService : ApplicationService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        ///     根据分类方法枚举获取分类
        /// </summary>
        /// <param name="taxonomy"></param>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<GetCategoryOutput> GetAllByTaxonomy(TaxonomyEnum taxonomy)
        {
            string cacheKey = "GetAllByTaxonomy_"+Enum.GetName(typeof(TaxonomyEnum), taxonomy);
            //创建缓存对象
            AsyncThreadSafeObjectCache<GetCategoryOutput> cache =
                new AsyncThreadSafeObjectCache<GetCategoryOutput>(System.Runtime.Caching.MemoryCache.Default, TimeSpan.FromSeconds(120));
            GetCategoryOutput output = await cache.GetAsync(cacheKey, async () => 
            {
                var tagCategory = await _categoryRepository.GetAllListAsync(w => w.Taxonomy == taxonomy);
                output = new GetCategoryOutput
                {
                    CategoryCollection = Mapper.Map<List<CategoryDto>>(tagCategory)
                };
                //插入到缓存
                cache.Set(cacheKey, output);
                //返回数据
                return output;
            });

            return output;
        }

        
        /// <summary>
        ///     获取所有的标签
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<GetCategoryOutput> GetAllTag()
        {
            return await GetAllByTaxonomy(TaxonomyEnum.PostTag);
        }

        /// <summary>
        ///     获取所有的分类
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<GetCategoryOutput> GetAllCategory()
        {
            return await GetAllByTaxonomy(TaxonomyEnum.Category);
        }

        
        /// <summary>
        ///     分页获取标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<CategoryPageOutput> GetTagByPage(CategoryPageInput input)
        {
            return await GetByPage(input, TaxonomyEnum.PostTag);
        }

        /// <summary>
        ///     分页获取分类和标签
        /// </summary>
        /// <param name="input">用户输入</param>
        /// <param name="taxonomy">分类类型</param>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<CategoryPageOutput> GetByPage(CategoryPageInput input, TaxonomyEnum taxonomy)
        {
            //跳过条数
            var skipCount = (input.PageIndex - 1) * input.PageSize;
            //获取条数
            var maxResultCount = input.PageSize;

            //获取总条数
            var totalCount = await _categoryRepository.CountAsync(w => w.Taxonomy == taxonomy);
            //获取集合
            var categoryList =
                await _categoryRepository.GetEnumerableByPage(skipCount, maxResultCount, taxonomy, o => o.CreationTime);
            var result = new CategoryPageOutput
            {
                TotalCount = totalCount,
                Items = Mapper.Map<IReadOnlyList<CategoryDto>>(categoryList),
                PageIndex = input.PageIndex
            };
            return result;
        }

        /// <summary>
        ///     分页获取分类数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(isTransactional: false)]
        public async Task<CategoryPageOutput> GetCategoryByPage(CategoryPageInput input)
        {
            return await GetByPage(input, TaxonomyEnum.Category);
        }

        /// <summary>
        ///     创建分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<RenderMessageOutput> CreateCategory(CreateCategoryInput input)
        {
            //校验分类是否已经存在
            var isExist =
                await _categoryRepository.CheckCategoryIsExist(input.CategoryName, input.ParentId, input.Taxonomy);

            if (isExist)
            {
                return new RenderMessageOutput
                {
                    Success = false,
                    Message = "操作失败，已经存在"
                };
            }

            Logger.Info("Creating a category for input : " + input);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = input.CategoryName,
                Description = input.Description,
                Taxonomy = input.Taxonomy
            };

            if (input.ParentId.HasValue)
            {
                category.ParentId = input.ParentId;
            }

            await _categoryRepository.InsertAsync(category);
            return new RenderMessageOutput(); //操作成功
        }

        /// <summary>
        ///     修改分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<RenderMessageOutput> UpdateCategory(UpdateCategoryInput input)
        {
            //写入日志
            Logger.Info("Updating a category for input : " + input);

            var category = await _categoryRepository.GetAsync(input.Id);
            if (category != null)
            {
                //校验分类是否已经存在
                var isExist =
                    await _categoryRepository.CheckCategoryIsExist(category.CategoryName, category.ParentId, category.Taxonomy);

                if (isExist && category.CategoryName == input.CategoryName )
                {
                    return new RenderMessageOutput
                    {
                        Success = false,
                        Message = "操作失败，已经存在"
                    };
                }

                if (!input.CategoryName.IsNullOrEmpty())
                {
                    category.CategoryName = input.CategoryName;
                }
                if (input.Order.HasValue)
                {
                    category.Order = (int)input.Order;
                }
                if (input.ParentId.HasValue)
                {
                    category.ParentId = input.ParentId;
                }
                if (!input.Description.IsNullOrEmpty())
                {
                    category.Description = input.Description;
                }
                if (input.Taxonomy.HasValue)
                {
                    category.Taxonomy = (TaxonomyEnum)input.Taxonomy;
                }
                if (input.Order.HasValue)
                {
                    category.Order = (int)input.Order;
                }

                await _categoryRepository.UpdateAsync(category);
                return new RenderMessageOutput();
            }
            else
            {
                return new RenderMessageOutput
                {
                    Success = false,
                    Message = "操作失败，分类不存在"
                };
            }
        }

        /// <summary>
        ///     删除分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task DeleteCategory(DeleteInput<Guid> input)
        {
            Logger.Info("Deleting category for input : " + input);
            await _categoryRepository.DeleteAsync(input.Id);
        }

        [UnitOfWork(isTransactional: false)]
        /// <summary>
        /// 获取一条分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CategoryOutput> GetCategory(GetCategoryInput input)
        {
            var category = await _categoryRepository.GetAsync(input.Id);
            return new CategoryOutput
            {
                Category = Mapper.Map<CategoryDto>(category)
            };
        }

    }
}