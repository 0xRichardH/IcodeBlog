using Abp.Web.Mvc.Controllers;

namespace Icode.Blog.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class BlogControllerBase : AbpController
    {
        protected BlogControllerBase()
        {
            LocalizationSourceName = BlogConsts.LocalizationSourceName;
        }
    }
}