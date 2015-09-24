using Abp.Web.Mvc.Views;

namespace Icode.Blog.Web.Views
{
    public abstract class BlogWebViewPageBase : BlogWebViewPageBase<dynamic>
    {

    }

    public abstract class BlogWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BlogWebViewPageBase()
        {
            LocalizationSourceName = BlogConsts.LocalizationSourceName;
        }
    }
}