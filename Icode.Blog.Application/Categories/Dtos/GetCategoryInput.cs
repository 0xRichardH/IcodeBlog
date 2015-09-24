using System;
using Abp.Application.Services.Dto;

namespace Icode.Blog.Categories.Dtos
{
    public class GetCategoryInput : IInputDto
    {
         public Guid Id { get; set; }

    }
}