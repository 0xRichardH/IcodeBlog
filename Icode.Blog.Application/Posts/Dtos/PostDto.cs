using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Icode.Blog.Categories;
using Icode.Blog.Comments;
using Icode.Blog.UserInfos;
using Newtonsoft.Json;
using Abp.Extensions;

namespace Icode.Blog.Posts.Dtos
{
    /// <summary>
    /// 文章
    /// </summary>
    public class PostDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 作者Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 当前的状态
        /// </summary>
        [Required]
        public PostState Status { get; set; }

        /// <summary>
        /// 当前文章状态的描述
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return this.Status.GetDescription();
            }
        }

        /// <summary>
        /// 评论设置状态
        /// </summary>
        [Required]
        public CommentSettingState CommentStatus { get; set; }

        /// <summary>
        ///文章密码（可选）
        /// </summary>
        [MaxLength(36)]
        public string Password { get; set; }

        /// <summary>
        /// 友好地址
        /// </summary>
        [MaxLength(50)]
        public string EntryName { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        [JsonIgnore]
        public string Except { get; set; }

        /// <summary>
        /// 是否在RSS显示
        /// </summary>
        [Required]
        public bool IsShowRSS { get; set; }

        /// <summary>
        /// 是否为原创文章
        /// </summary>
        public bool IsOriginal { get; set; }

        /// <summary>
        /// 是否置顶显示
        /// </summary>
        [Required]
        public bool IsTop { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public long CommentCount { get; set; }

        /// <summary>
        /// PV访问量
        /// </summary>
        public long PVCount { get; set; }

        ///// <summary>
        ///// 评论的集合
        ///// </summary>
        //[JsonIgnore]
        //public ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// 分类的集合
        /// </summary>
        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }
    }
}