using Abp.Application.Services.Dto;
using System;

namespace Icode.Blog.Posts.Dtos
{
    /// <summary>
    /// 获取前十篇最新文章
    /// </summary>
    public class TopTenPostDto : EntityDto<Guid>
    {
        public string Title { get; set; }

        public string EntryName { get; set; }
    }
}
