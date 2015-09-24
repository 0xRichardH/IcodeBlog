using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using System;

namespace Icode.Blog.Posts.Dtos
{
    /// <summary>
    /// 添加文章
    /// </summary>
    public class CreatePostInput : IInputDto
    {
        /// <summary>
        /// 文章Id，编辑文章是需要
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 文章分类的集合
        /// </summary>
        public Guid[] CategoryArray { get; set; }

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
        public PostState Status { get; set; } = PostState.AutoDraft;

        /// <summary>
        /// 评论设置状态
        /// </summary>
        [Required]
        public CommentSettingState CommentStatus { get; set; } = CommentSettingState.RegisteredOnley;

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
        public string Except { get; set; }

        /// <summary>
        /// 是否在RSS显示
        /// </summary>
        [Required]
        public bool IsShowRSS { get; set; } = false;

        /// <summary>
        /// 是否置顶显示
        /// </summary>
        [Required]
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// 是否为原创
        /// </summary>
        [Required]
        public bool IsOriginal { get; set; } = true;


        public override string ToString()
        {
            return string.Format("CreatePostInput => [ Title = {0} , UserId = {1} , Content = {2} , Status = {3} , CommentStatus = {4} , Password = {4} ,  EntryName = {5} , IsShowRSS = {6}  , IsTop = {7}  ]",
                this.Title,this.UserId,this.Content,this.Status,this.CommentStatus,this.Password,this.EntryName,this.IsShowRSS,this.IsTop);
        }
    }
}