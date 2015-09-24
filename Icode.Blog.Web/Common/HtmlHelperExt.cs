using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// HtmlHelper扩张类
    /// </summary>
    public static class HtmlHelperExt
    {
        /// <summary>
        /// 渲染页码
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageIndex"></param> 
        /// <param name="pageSize"></param>
        /// <param name="totalPager"></param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageIndex, int pageSize, int totalPager,string actionUrl)
        {
            //对用的输入进行处理
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            actionUrl = HttpContext.Current.Server.UrlDecode(actionUrl);//Url解码

            StringBuilder builder = new StringBuilder();//最终的页码

            //拼接一个div
            builder.Append("<div class=\"container\">");
            builder.Append(" <ul class=\"pagination\">");

            if (pageIndex != 1)
            {
                var url = string.Format(actionUrl,pageIndex-1);
                //上一页
                builder.AppendFormat("<li><a href=\"{0}\">&laquo;</a></li>", url);
            }
            else
            {
                //上一页
                builder.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\">&laquo;</a></li>");
            }

            //显示的页码8
            int _page = 8;

            int _start = 1;
            int _end = _page;
            if (pageIndex > _page)//页面大于预先指定的页码值
            {
                int oper = (int)Math.Ceiling(_page * 1.0D / 2);//天花板函数取系数
                if ((pageIndex + oper) > totalPager)
                {
                    _start = pageIndex - _page + 1;
                    _end = pageIndex;
                }
                else
                {
                    _start = pageIndex - oper + 1;
                    _end = pageIndex + oper;
                }
            }
            if (_end > totalPager)//最后的页码超出总页面进行处理
            {
                _end = totalPager;
                if (totalPager > _page)
                {
                    _start = totalPager - _page;
                }
            }

            //生成数字页码
            for (int i = _start; i <= _end; i++)
            {
                if (pageIndex == i)
                {
                    builder.AppendFormat(" <li class=\"active\"><a href=\"javascript:void(0)\">{0}<span class=\"sr-only\">(current)</span></a></li>", i);
                }
                else
                {
                    var url = string.Format(actionUrl, i);
                    builder.AppendFormat(" <li><a href=\"{1}\">{0}<span class=\"sr-only\">(current)</span></a></li>", i, url);
                }
            }


            if (pageIndex != totalPager)
            {
                var url = string.Format(actionUrl, pageIndex+1);
                //下一页
                builder.AppendFormat(" <li><a href=\"{0}\">&raquo;</a></li>", url);
            }
            else
            {
                //下一页
                builder.Append("<li class=\"disabled\"><a href=\"javascript:void(0)\">&raquo;</a></li>");
            }

            builder.Append("</ul>");
            builder.Append("</div>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}