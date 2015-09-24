using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Posts;
using Icode.Blog.UserInfos;

namespace Icode.Blog.Comments
{
    /// <summary>
    /// 评论信息表
    /// </summary>
    public class Comment : FullAuditedEntity<Guid,UserInfo>
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        [Required]
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        /// <summary>
        /// 文章Id
        /// </summary>
        public virtual  Guid PostId { get; set; }

        /// <summary>
        /// 评论者
        /// </summary>
        [MaxLength(50)]
        public virtual string Author { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [EmailAddress]
        [MaxLength(50)]
        public virtual string Email { get; set; }

        /// <summary>
        /// 评论者网址
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 评论者IP
        /// </summary>
        public virtual string IP { get; set; }

        /// <summary>
        /// 父评论
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Comment ParentComment { get; set; }

        /// <summary>
        /// 父评论Id
        /// </summary>
        public virtual  Guid? ParentId { get; set; }

        /// <summary>
        /// 用户信息(如果用户注册)
        /// </summary>
        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual  long? UserId { get; set; }
    }
}