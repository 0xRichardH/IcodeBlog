using Icode.Blog.Categories;
using Icode.Blog.Posts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Icode.Blog.Web.Controllers
{
    /// <summary>
    /// 分布视图
    /// </summary>
    public class PartialController : BlogControllerBase
    {
        //文章
        private readonly IPostService _postService;
        //分类
        private readonly ICategoryService _categoryService;

        public PartialController(IPostService postService, ICategoryService categoryService)
        {
            this._postService = postService;
            this._categoryService = categoryService;
        }

        /// <summary>
        /// RightPage.cshtml 分部视图渲染
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public async Task<ActionResult> RenderRightPage()
        {
            var topTenOutput = await this._postService.GetTopTen();
            var categoryOutput = await this._categoryService.GetAllCategory();

            ViewBag.TopPost = topTenOutput.Items;//前十条文章
            ViewBag.Categories = categoryOutput.CategoryCollection;//所有的分类
            return PartialView("~/Views/Shared/RightPage.cshtml");
        }
    }
}