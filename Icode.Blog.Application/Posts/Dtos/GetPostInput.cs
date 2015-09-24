using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Icode.Blog.Posts.Dtos
{
    public class GetPostInput : IInputDto
    {
        /// <summary>
        /// Id或EntryName
        /// </summary>
        [Required]
        public string IdOrEntryName { get; set; }
    }
}
