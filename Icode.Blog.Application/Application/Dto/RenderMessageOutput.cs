using Abp.Application.Services.Dto;

namespace Icode.Blog.Application.Dto
{
    /// <summary>
    /// 向用户输出消息
    /// </summary>
    public class RenderMessageOutput : IOutputDto
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }

    }
}