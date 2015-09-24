using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Icode.Blog.Categories.Dtos
{
    public class GetCategoryOutput : IOutputDto
    {
        public IReadOnlyList<CategoryDto> CategoryCollection { get; set; }
    }
}