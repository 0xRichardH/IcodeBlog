using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Icode.Blog.Posts;
using Newtonsoft.Json;

namespace Icode.Blog.Categories.Dtos
{
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        [JsonIgnore]
        public Category CategoryParent { get; set; }

        /// <summary>
        /// 父分类的名称
        /// </summary>
        public string ParentCategoryName
        {
            get
            {
                if (CategoryParent != null)
                {
                    return CategoryParent.CategoryName;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类的详细说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public  int Order { get; set; }

        /// <summary>
        /// 文章数统计
        /// </summary>
        public  int Count { get; set; }

        /// <summary>
        /// 分类方法
        /// </summary>
        public  TaxonomyEnum Taxonomy { get; set; }

        ///// <summary>
        ///// 文章的集合
        ///// </summary>
        //[JsonIgnore]
        //public ICollection<Post> Posts { get; set; }

    }
}