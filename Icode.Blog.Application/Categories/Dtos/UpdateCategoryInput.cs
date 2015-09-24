using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Icode.Blog.Categories.Dtos
{
    /// <summary>
    /// 修改分类数据，用户输入
    /// </summary>
    public class UpdateCategoryInput : IInputDto,ICustomValidate
    {
        [Required]
         public Guid Id { get; set; }

        /// <summary>
        /// 父分类ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 分类详细地址
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 分类方式
        /// </summary>
        public TaxonomyEnum? Taxonomy { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// 校验规则
        /// </summary>
        /// <param name="results"></param>
        public void AddValidationErrors(List<ValidationResult> results)
        {
            
        }

        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("UpdateCategoryInput => Id = {0} , ParentId = {1} , CategoryName = {2} , Description = {3} , Taxonomy = {4} , Order = {5}",
                Id,
                ParentId,
                CategoryName,
                Description,
                Taxonomy,
                Order);
        }
    }
}