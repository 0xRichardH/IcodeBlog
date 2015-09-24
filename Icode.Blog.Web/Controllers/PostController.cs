using Icode.Blog.Categories;
using Icode.Blog.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Icode.Blog.Web.Controllers
{
    public class PostController : BlogControllerBase
    {
        public readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        /// <summary>
        /// 显示文章的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Articles(string id)
        {
            var post = await this._postService.GetPostByIdOrEntryName(new Posts.Dtos.GetPostInput { IdOrEntryName = id });
            ViewBag.Post = post.Post;//文章
            ViewBag.Tags = post.Post.Categories.Where(w=>w.Taxonomy == TaxonomyEnum.PostTag);//文章的标签
            return View("Article");
        }
    }
}