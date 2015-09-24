using Icode.Blog.Posts;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Icode.Blog.Web.Controllers
{
    /// <summary>
    /// 网站首页
    /// </summary>
    public class HomeController : BlogControllerBase
    {
        //文章
        public readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            this._postService = postService;
        }

        public async Task<ActionResult> Index(int? id)
        {
            int pageIndex = 1;
            int pageSize = 10;
            int totalPage = 0;
            if (id.HasValue)
            {
                pageIndex = (int)id;
            }

            //分页获取文章
            var postOutput = await this._postService.PostList(new Posts.Dtos.GetPostListInput
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            
            totalPage = (int)Math.Ceiling(postOutput.TotalCount / (pageSize * 1.0 ));

            //判断是否为合法页数
            if(pageIndex > totalPage)
            {
                throw new HttpException(404, "Not Found");
            }

            ViewBag.Posts = postOutput.Items;
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalPage = totalPage;
            ViewBag.PageSize = pageSize;
            return View("Index");
        }

        /// <summary>
        /// 404页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            Logger.ErrorFormat("用户Ip:{0},访问错误页面：{1}", Request.UserHostAddress, Request["aspxerrorpath"]);
            return View();
        }

    }
}
