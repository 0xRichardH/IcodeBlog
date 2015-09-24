using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Categories;
using Icode.Blog.UserInfos;

namespace Icode.Blog.CategoryMetas
{
    /// <summary>
    /// 文章分类扩展表
    /// </summary>
    public class CategoryMeta : FullAuditedEntity<Guid, UserInfo>
    {
        /// <summary>
        /// 对应的文章分类
        /// </summary>
        [Required]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// 对应的文章分类Id
        /// </summary>
        public virtual Guid CategoryId { get; set; }

        /// <summary>
        /// 键名
        /// </summary>
        [Required]
        public virtual string MetaKey { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public virtual string MetaValue { get; set; }
    }
}