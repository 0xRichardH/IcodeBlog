using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Icode.Blog.Posts.Dtos
{
    public class GetPostListInput : IInputDto,ICustomValidate
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 一页数据条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 对用户的输入进行校验
        /// </summary>
        /// <param name="results"></param>
        public void AddValidationErrors(List<ValidationResult> results)
        {
            //容错性，当用户输入不合法的数值时，也能正常调用，保证用户的体验
            if (this.PageIndex <= 0)
            {
                this.PageIndex = 1;
            }
            if (this.PageSize <= 0)
            {
                this.PageSize = 10;
            }
        }
    }
}