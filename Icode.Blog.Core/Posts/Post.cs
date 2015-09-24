using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Categories;
using Icode.Blog.Comments;
using Icode.Blog.UserInfos;

namespace Icode.Blog.Posts
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public class Post : FullAuditedEntity<Guid, UserInfo>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        /// <summary>
        /// 作者Id
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public virtual string Content { get; set; }

        /// <summary>
        /// 当前的状态
        /// </summary>
        [Required]
        public virtual PostState Status { get; set; }

        /// <summary>
        /// 评论设置状态
        /// </summary>
        [Required]
        public virtual CommentSettingState CommentStatus { get; set; }

        /// <summary>
        ///文章密码（可选）
        /// </summary>
        [MaxLength(36)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 友好地址
        /// </summary>
        [MaxLength(50)]
        public virtual string EntryName { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        public virtual string Except { get; set; }

        /// <summary>
        /// 是否在RSS显示
        /// </summary>
        [Required]
        public virtual bool IsShowRSS { get; set; }

        /// <summary>
        /// 是否置顶显示
        /// </summary>
        [Required]
        public virtual bool IsTop { get; set; }

        /// <summary>
        /// 是否为原创文章
        /// </summary>
        public virtual bool IsOriginal { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public virtual long CommentCount { get; set; }

        /// <summary>
        /// PV访问量
        /// </summary>
        public virtual long PVCount { get; set; }

        /// <summary>
        /// 评论的集合
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// 分类的集合
        /// </summary>
        public virtual ICollection<Category> Categories { get; set; }
    }
}