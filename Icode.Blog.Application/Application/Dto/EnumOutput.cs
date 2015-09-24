using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icode.Blog.Posts.Dtos
{
    /// <summary>
    /// 枚举状态
    /// </summary>
    public class EnumOutput : IOutputDto
    {
        /// <summary>
        /// 返回枚举字典集合
        /// </summary>
        public IReadOnlyDictionary<int, string> Dict { get; set; }

        /// <summary>
        /// values值的集合
        /// </summary>
        public int[] Values { get; set; }
    }
}
