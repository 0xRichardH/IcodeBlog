using Abp.Application.Services.Dto;

namespace Icode.Blog.Posts.Dtos
{
    public class GetPostOutput : IOutputDto
    {
        public PostDto Post { get; set; }
    }
}
