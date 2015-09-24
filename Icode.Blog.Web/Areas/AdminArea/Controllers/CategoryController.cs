using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Controllers;

namespace Icode.Blog.Web.Areas.AdminArea.Controllers
{
    /// <summary>
    /// 文章分类操作
    /// </summary>
    public class CategoryController : AdminAreaContrillerBase
    {
        /// <summary>
        /// 获得分类列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Category()
        {
            ViewBag.TypeTitle = "分类";
            ViewBag.Js = "category_category.js";
            //新增的链接
            ViewBag.CreateUrl = Url.Action("createCategory");
            //编辑链接
            ViewBag.EditUrl = Url.Action("editCategory");
            return View("category");
        }

        /// <summary>
        /// 获得标签列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Tag()
        {
            ViewBag.TypeTitle = "标签";
            ViewBag.Js = "category_tag.js";
            //新增的链接
            ViewBag.CreateUrl = Url.Action("createTag");
            //编辑链接
            ViewBag.EditUrl = Url.Action("editTag");
            return View("category");
        }

        /// <summary>
        /// 新建分类的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateCategory()
        {
            ViewBag.OptionTitle = "新增";
            ViewBag.TypeTitle = "分类";
            ViewBag.RedirectUrl = Url.Action("category");
            ViewBag.IsCategory = true;
            return View("createOrEdit");
        }

        /// <summary>
        /// 新建标签的页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateTag()
        {
            ViewBag.OptionTitle = "新增";
            ViewBag.TypeTitle = "标签";
            ViewBag.RedirectUrl = Url.Action("tag");
            ViewBag.IsCategory = false;
            return View("createOrEdit");
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCategory(Guid id)
        {
            ViewBag.OptionTitle = "编辑";
            ViewBag.TypeTitle = "分类";
            ViewBag.Id = id;//需要编辑的Id
            ViewBag.RedirectUrl = Url.Action("category");
            ViewBag.IsCategory = true;
            return View("createOrEdit");   
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTag(Guid id)
        {
            ViewBag.OptionTitle = "编辑";
            ViewBag.TypeTitle = "标签";
            ViewBag.Id = id;//需要编辑的Id
            ViewBag.RedirectUrl = Url.Action("tag");
            ViewBag.IsCategory = false;
            return View("createOrEdit");
        }
    }
}