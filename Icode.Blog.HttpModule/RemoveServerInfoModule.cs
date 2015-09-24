using System.Web;

namespace Icode.Blog.HttpModule
{
    /// <summary>
    /// 移除Server http返回头
    /// </summary>
    public class RemoveServerInfoModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += Context_PreSendRequestHeaders;
        }

        private void Context_PreSendRequestHeaders(object sender, System.EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }

        public void Dispose()
        {
            
        }
    }
}