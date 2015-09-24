using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Posts;
using Icode.Blog.UserInfos;

namespace Icode.Blog.PostMetas
{
    /// <summary>
    /// 文章内容拓展类
    /// </summary>
    public class PostMeta : FullAuditedEntity<Guid,UserInfo>
    {
        /// <summary>
        /// 对应文章
        /// </summary>
        [Required]
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        /// <summary>
        /// 对应文章Id
        /// </summary>
        public virtual Guid PostId { get; set; }

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