using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icode.Blog.Posts.Dtos
{
    /// <summary>
    /// 获取前十篇最新文章Output
    /// </summary>
    public class GetTopTenOutput : IOutputDto
    {
        public IReadOnlyList<TopTenPostDto> Items { get; set; }
    }
}
