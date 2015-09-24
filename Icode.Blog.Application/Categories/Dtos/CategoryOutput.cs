using Abp.Application.Services.Dto;

namespace Icode.Blog.Categories.Dtos
{
    public class CategoryOutput : IOutputDto
    {
         public CategoryDto Category { get; set; }
    }
}