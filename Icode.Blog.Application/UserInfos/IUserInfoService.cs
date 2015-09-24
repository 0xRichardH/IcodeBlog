using Abp.Application.Services;
using Icode.Blog.UserInfos.Dto;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Icode.Blog.UserInfos
{
    public interface IUserInfoService : IApplicationService
    {
        /// <summary>
        /// 创建ClaimsIdentity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        ClaimsIdentity CreateIdentity(UserInfoDto user, string authenticationType);

        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserInfoOutput> GetUserInfoByEmail(UserInfoInput input);

        /// <summary>
        /// 生成用户验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserValidateCodeOutput> GenderValidateCode(UserValidateCodeInput input);

        /// <summary>
        /// 校验验证码是否正确
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CheckValidateCode(UserValidateCodeInput input);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlBody"></param>
        /// <returns></returns>
        Task SendEmailAsync(string to, string subject, string body, bool isHtmlBody = true);
    }
}
