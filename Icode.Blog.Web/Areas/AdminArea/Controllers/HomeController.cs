using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Icode.Blog.Web.Controllers;

namespace Icode.Blog.Web.Areas.AdminArea.Controllers
{
    public class HomeController : AdminAreaContrillerBase
    {
        // GET: AdminArea/Home
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}