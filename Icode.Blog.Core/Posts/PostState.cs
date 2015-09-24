using System.ComponentModel;

namespace Icode.Blog.Posts
{
    /// <summary>
    /// 文章状态
    /// </summary>
    public enum PostState 
    {
        /// <summary>
        /// 已发表
        /// </summary>
        [Description("已发表")]
        Publish = 0,
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Draft = 1,
        /// <summary>
        /// 自动保存草稿
        /// </summary>
        [Description("自动保存草稿")]
        AutoDraft = 2,
        /// <summary>
        /// 私人内容
        /// </summary>
        [Description("私人内容")]
        Private = 3,
        /// <summary>
        /// 回收站
        /// </summary>
        [Description("回收站")]
        Trash = 4
    }
}