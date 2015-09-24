using System.ComponentModel;

namespace Icode.Blog.Posts
{
    /// <summary>
    /// 评论设置状态
    /// </summary>
    public enum CommentSettingState
    {
        /// <summary>
        /// 允许评论
        /// </summary>
        [Description("允许评论")]
        Open = 0,
        /// <summary>
        /// 不允许评论
        /// </summary>
        [Description("不允许评论")]
        Closed = 1,
        /// <summary>
        /// 只有注册用户方可评论
        /// </summary>
        [Description("注册用户可评论")]
        RegisteredOnley = 2

    }
}