using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Icode.Blog.Posts.Dtos
{
    public class GetPostListOutput : IOutputDto
    {
        public IReadOnlyList<PostDto> Items { get; set; }

        public int TotalCount { get; set; }

        public int PageIndex { get; set; }
    }
}