using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Icode.Blog.Categories.Dtos
{
    /// <summary>
    /// 返回的是获取数据
    /// </summary>
    public class CategoryPageOutput : IOutputDto
    {
        public IReadOnlyList<CategoryDto> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }
    }
}