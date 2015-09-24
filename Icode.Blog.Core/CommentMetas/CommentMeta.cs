using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Comments;
using Icode.Blog.Posts;
using Icode.Blog.UserInfos;

namespace Icode.Blog.CommentMetas
{
    /// <summary>
    /// 评论信息拓展表
    /// </summary>
    public class CommentMeta : FullAuditedEntity<Guid,UserInfo>
    {
        /// <summary>
        /// 对应评论
        /// </summary>
        [Required]
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        /// <summary>
        /// 对应评论Id
        /// </summary>
        public virtual  Guid CommentId { get; set; }

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