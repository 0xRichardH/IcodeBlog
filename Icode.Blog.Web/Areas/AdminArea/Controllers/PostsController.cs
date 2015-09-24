using System.Web.Mvc;
using Abp.Extensions;

namespace Icode.Blog.Web.Areas.AdminArea.Controllers
{
    /// <summary>
    /// 文章操作
    /// </summary>
    public class PostsController : AdminAreaContrillerBase
    {
        /// <summary>
        /// 获取文章的列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return View("PostsList");
        }

        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(string id)
        {
            ViewBag.Id = id;
            if(id.IsNullOrEmpty())
            {
                ViewBag.Name = "添加";
            }
            else
            {
                ViewBag.Name = "编辑";
            }
            return View("CreateOrEditPost");
        }
    }
}