using Abp.Application.Services.Dto;

namespace Icode.Blog.UserInfos.Dto
{
    public class UserInfoOutput : IOutputDto
    {
        public UserInfoDto UserInfo { get; set; }
    }
}
