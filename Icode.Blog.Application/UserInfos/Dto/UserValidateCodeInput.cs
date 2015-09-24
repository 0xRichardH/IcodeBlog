using Abp.Application.Services.Dto;

namespace Icode.Blog.UserInfos.Dto
{
    /// <summary>
    /// 生成用户验证码（用户输入）
    /// </summary>
    public class UserValidateCodeInput : IInputDto
    {
        /// <summary>
        /// 用户名或邮箱
        /// </summary>
        public string NameOrEmail { get; set; }

        /// <summary>
        /// 页面的Session值
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 当前页面的链接
        /// </summary>
        public string HostUrl { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VCode { get; set; }
    }
}
