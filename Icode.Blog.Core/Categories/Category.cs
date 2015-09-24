using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.Posts;
using Icode.Blog.UserInfos;

namespace Icode.Blog.Categories
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class Category : FullAuditedEntity<Guid,UserInfo>
    {
        /// <summary>
        /// 分类的父编码
        /// </summary>
         public virtual Guid? ParentId { get; set; }    

        /// <summary>
        /// 父分类
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Category CategoryParent { get; set; }


        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [MaxLength(36)]
        public virtual  string CategoryName { get; set; }

        /// <summary>
        /// 分类详细说明
        /// </summary>
        public virtual  string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual  int Order { get; set; }

        /// <summary>
        /// 文章数统计
        /// </summary>
        public virtual  int Count { get; set; }

        /// <summary>
        /// 分类方法
        /// </summary>
        public virtual TaxonomyEnum Taxonomy { get; set; }

        /// <summary>
        /// 文章的集合
        /// </summary>
        public virtual  ICollection<Post> Posts { get; set; }

        public Category()
        {
            this.Taxonomy = TaxonomyEnum.Category;
        }
    }
}