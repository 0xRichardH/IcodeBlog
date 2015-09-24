using Abp.Application.Services.Dto;

namespace Icode.Blog.UserInfos.Dto
{
    /// <summary>
    /// 最后生成的验证码
    /// </summary>
    public class UserValidateCodeOutput : IOutputDto
    {
        public string ValidateCode { get; set; }
    }
}
