using System.ComponentModel;

namespace Icode.Blog.Categories
{
    /// <summary>
    /// 分类方法枚举
    /// </summary>
    public enum TaxonomyEnum
    {
        /// <summary>
        /// 分类
        /// </summary>
        [Description("分类")]
        Category = 0,
        /// <summary>
        /// 标签
        /// </summary>
        [Description("标签")]
        PostTag = 1
    }
}