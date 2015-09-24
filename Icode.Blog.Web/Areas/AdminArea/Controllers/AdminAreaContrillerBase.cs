using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;

namespace Icode.Blog.Web.Areas.AdminArea.Controllers
{
    /// <summary>
    /// 后台管理控制器基类
    /// </summary>
    [AbpMvcAuthorize]
    public class AdminAreaContrillerBase : AbpController
    {
         
    }
}