namespace System.Web.Mvc
{
    /// <summary>
    /// 页面帮助类
    /// </summary>
    public static class PageCommonHelper
    {
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormateDateTime(DateTime? dateTime)
        {
            return dateTime.HasValue ? ((DateTime)dateTime).ToString("yyyy-MM-dd") : string.Empty;
        }

        /// <summary>
        /// 生成文章的链接
        /// </summary>
        /// <param name="entryName">友好链接</param>
        /// <param name="id">文章Id</param>
        /// <returns></returns>
        public static string RenderPostUrl(string entryName,string id)
        {
            return string.IsNullOrEmpty(entryName) ?id : entryName;
        }
    }
}