using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Icode.Blog.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            //index.html 首页默认路由
            routes.MapRoute(
                name: "Index",
                url: "index.html",
                defaults: new { controller = "home", action = "index", id= UrlParameter.Optional },
                namespaces: new[] { "Icode.Blog.Web.Controllers" }
            );

            //404.html 404错误页配置
            routes.MapRoute(
                name: "404",
                url: "404.html",
                defaults: new { controller = "home", action = "error404", id = UrlParameter.Optional },
                namespaces: new[] { "Icode.Blog.Web.Controllers" }
            );

            //首页分页路由
            routes.MapRoute(
                name: "Index_Page",
                url: "index_{id}.html",
                defaults: new { controller = "home", action = "index",id = UrlParameter.Optional },
                namespaces: new[] { "Icode.Blog.Web.Controllers" }
            );

            //文章详情的路由
            routes.MapRoute(
                name: "Articles",
                url: "articles/{id}.html",
                defaults: new { controller = "post", action = "articles", id = UrlParameter.Optional },
                namespaces: new[] { "Icode.Blog.Web.Controllers" }
            );

            //默认路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "Icode.Blog.Web.Controllers" }
            );

        }
    }
}
