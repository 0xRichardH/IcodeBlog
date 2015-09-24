using System.Web.Mvc;

namespace Icode.Blog.Web.Areas.AdminArea
{
    public class AdminAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //index
            context.MapRoute(
                "AdminArea_Index",
                "adminarea/index.html",
                new
                {
                    action = "index",
                    controller = "home"
                },
                new[] { "Icode.Blog.Web.Areas.AdminArea.Controllers" }
            );
            //默认路由
            context.MapRoute(
                "AdminArea_default",
                "adminarea/{controller}_{action}.html",
                new
                {
                    action = "index",
                    controller = "home"
                },
                new[] { "Icode.Blog.Web.Areas.AdminArea.Controllers" }
            );
        }
    }
}