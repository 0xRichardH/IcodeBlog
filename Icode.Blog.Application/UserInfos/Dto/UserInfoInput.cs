using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Icode.Blog.UserInfos.Dto
{
    public class UserInfoInput : IInputDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
