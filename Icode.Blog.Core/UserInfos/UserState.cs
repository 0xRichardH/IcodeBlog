using System.ComponentModel;

namespace Icode.Blog.UserInfos
{
    /// <summary>
    /// 用户状态枚举值
    /// </summary>
    public enum UserState 
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable=0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable=1
    }
}