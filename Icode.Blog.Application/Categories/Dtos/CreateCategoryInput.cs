using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Icode.Blog.Categories.Dtos
{
    /// <summary>
    /// 创建分类Dto
    /// </summary>
    public class CreateCategoryInput : IInputDto, ICustomValidate
    {
        /// <summary>
        /// 父分类编码
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类的详细说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 分类方法
        /// </summary>
        public TaxonomyEnum Taxonomy { get; set; }

        /// <summary>
        /// override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[ CreateCategoryInput => ParentId = {0} , CategoryName = {1} , Description = {2} , Taxonomy = {3}  ]",
                this.ParentId, this.CategoryName, this.Description, this.Taxonomy);
        }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            
        }
    }
}