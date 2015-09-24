using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Icode.Blog.Categories;
using Icode.Blog.Categories.Dtos;
using Shouldly;
using Xunit;

namespace Icode.Blog.Test.Categories
{
    /// <summary>
    /// 测试 类CategoryService.cs
    /// </summary>
    public class CategoryServiceTests : BlogTestBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryService = LocalIocManager.Resolve<ICategoryService>();
        }

        [Fact]
        public async void Should_GetAllCategoies()
        {
            //运行方法
            var output = await _categoryService.GetAllCategory();
            var output2 = await _categoryService.GetAllTag();

            //Checking results
            output.CategoryCollection.Count.ShouldBe(1);
            output2.CategoryCollection.Count.ShouldBe(0);
        }

        [Fact]
        public async void Should_CreateCategory()
        {
            //初始化的分类条数
            var initCategoryCount = base.UsingDbContext(context => context.Category.Count());

            //增加三条分类条数
            var input1 = new CreateCategoryInput
            {
                ParentId = base.UsingDbContext(context =>
               {
                   return context.Category.FirstOrDefault().Id;
               }),
                CategoryName = "人文历史",
                Description = "这个是用来介绍人文历史的",
                Taxonomy = TaxonomyEnum.Category
            };
            var task1 = _categoryService.CreateCategory(input1);

            var input2 = new CreateCategoryInput()
            {
                CategoryName = "社会科学",
                Description = "这个就是介绍社会科学的啦",
                Taxonomy = TaxonomyEnum.Category
            };
            var task2 = _categoryService.CreateCategory(input2);

            var input3 = new CreateCategoryInput()
            {
                CategoryName = "新技术",
                Taxonomy = TaxonomyEnum.PostTag
            };
            var task3 = _categoryService.CreateCategory(input3);

            await Task.WhenAll(task1, task2, task3);

            //校验规则
            base.UsingDbContext(context =>
            {
                context.Category.Count().ShouldBe(initCategoryCount + 3);
                var c1 = context.Category.Where(w => w.CategoryName == "人文历史").FirstOrDefault();
                c1.ParentId.HasValue.ShouldBe(true);
                c1.Taxonomy.ShouldBe(TaxonomyEnum.Category);
                c1.Description.IsNullOrEmpty().ShouldBe(false);

                var c2 = context.Category.FirstOrDefault(w => w.CategoryName == "社会科学");
                c2.ParentId.HasValue.ShouldBe(false);
                c2.Taxonomy.ShouldBe(TaxonomyEnum.Category);
                c2.Description.IsNullOrEmpty().ShouldBe(false);

                var c3 = context.Category.FirstOrDefault(w => w.CategoryName == "新技术");
                c3.ParentId.HasValue.ShouldBe(false);
                c3.Taxonomy.ShouldBe(TaxonomyEnum.PostTag);
                c3.Description.IsNullOrEmpty().ShouldBe(true);
            });

        }

        [Fact]
        public async void Should_UpdateCategory()
        {
            var categoryDto = new UpdateCategoryInput()
            {
                Id = Guid.Parse("81f79e33-e179-4038-bbb5-1a90be081ca9"),
                CategoryName = "修改后的"
            };
            await _categoryService.UpdateCategory(categoryDto);

            //测试是否修改成功
            UsingDbContext(context =>
            {
                context.Category.Count().ShouldBe(1);
                var category = context.Category.FirstOrDefault();
                category.CategoryName.ShouldBe("修改后的");
                category.Id.ShouldBe(Guid.Parse("81f79e33-e179-4038-bbb5-1a90be081ca9"));
                category.Description.ShouldBe("用来发布文章的");
                category.Order.ShouldBe(0);
                category.ParentId.ShouldBe(null);
            });
        }

        [Fact]
        public async void Should_GetByPage()
        {
            var input1 = new CategoryPageInput()
            {
                PageIndex = 1,
                PageSize = 5
            };

            var input2 = new CategoryPageInput()
            {
                PageIndex = 0,
                PageSize = 0
            };

            var input3 = new CategoryPageInput()
            {
                PageIndex = 2,
                PageSize = 5
            };

            var output1 = await _categoryService.GetCategoryByPage(input1);
            var output2 = await _categoryService.GetCategoryByPage(input2);
            var output3 = await _categoryService.GetCategoryByPage(input3);

            output1.TotalCount.ShouldBe(1);
            output1.TotalCount.ShouldBe(output2.TotalCount);
            output1.Items.Count.ShouldBe(output2.Items.Count);
            output1.Items.Count().ShouldBe(1);
            output3.TotalCount.ShouldBe(1);
            output3.Items.Count.ShouldBe(0);
        }
    }
}