using System;
using System.Security.Claims;
using Abp.Application.Services;
using Icode.Blog.UserInfos.Dto;
using System.Threading.Tasks;
using System.Linq;
using Abp.AutoMapper;
using Abp.Encrypt;
using Abp.Threading;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;

namespace Icode.Blog.UserInfos
{
    public class UserInfoService : ApplicationService, IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            this._userInfoRepository = userInfoRepository;
        }

        /// <summary>
        /// 创建ClaimsIdentity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateIdentity(UserInfoDto user, string authenticationType)
        {
            ClaimsIdentity identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            identity.AddClaim(new Claim("DisplayName", user.NickName));
            return identity;
        }

        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserInfoOutput> GetUserInfoByEmail(UserInfoInput input)
        {
            var userInfos = await this._userInfoRepository.GetAllListAsync(w => w.Email == input.Email);
            return new UserInfoOutput
            {
                UserInfo = userInfos.FirstOrDefault().MapTo<UserInfoDto>()
            };
        }

        /// <summary>
        /// 内部生成验证码
        /// </summary>
        /// <param name="nameOrEmail">用户名或邮箱</param>
        /// <param name="sessionId">页面的Session值</param>
        /// <param name="hostUrl">页面请求的链接</param>
        /// <param name="dateTime">当前的时间</param>
        /// <returns>六位验证码</returns>
        private async Task<string> InnerGenderValidateCode(string nameOrEmail, string sessionId, string hostUrl, DateTime dateTime)
        {
            return await Task.Run(() =>
            {
                nameOrEmail = EncryptHelper.HashEncoding(nameOrEmail);
                hostUrl = EncryptHelper.Md5(hostUrl);
                string dateString = EncryptHelper.SHA1EncryptBy(dateTime.ToString("yyyyMMddHHmm"));

                string validateCode = string.Format("sessionId:{0},name:{1},date:{2},host:{3}", sessionId, nameOrEmail, dateString, hostUrl);
                validateCode = EncryptHelper.HashEncoding(validateCode).Replace("O", "");
                return validateCode.Substring(0, 3) + validateCode.Substring(validateCode.Length - 3, 3);
            });
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserValidateCodeOutput> GenderValidateCode(UserValidateCodeInput input)
        {
            string vCode = await this.InnerGenderValidateCode(input.NameOrEmail, input.SessionId, input.HostUrl, DateTime.Now);

            return new UserValidateCodeOutput
            {
                ValidateCode = vCode
            };
        }

        /// <summary>
        /// 校验验证码是否正确
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CheckValidateCode(UserValidateCodeInput input)
        {
            bool result = await Task.Run(async () =>
           {
               bool checkResult = false;
               var dateTime = DateTime.Now;
               for (int i = 0; i < 5; i++)
               {
                   string vCode = await this.InnerGenderValidateCode(input.NameOrEmail, input.SessionId, input.HostUrl, dateTime);
                   if (vCode.ToLower() == input.VCode.ToLower())
                   {
                        //校验通过
                        checkResult = true;
                       break;
                   }
                   dateTime = dateTime.AddMinutes(-1);
               }
               return checkResult;
           });

            return result;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtmlBody"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string to, string subject, string body, bool isHtmlBody = true)
        {
            await Task.Run(() =>
            {
                MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
                mailMsg.From = new MailAddress("service@haoxilu.net", "haoxilu.net");//源邮件地址(发件人) 
                mailMsg.To.Add(new MailAddress(to, to));//目的邮件地址。可以有多个收件人
                mailMsg.Subject = subject;//发送邮件的标题 
                mailMsg.Body = body;//发送邮件的内容 
                mailMsg.IsBodyHtml = isHtmlBody;
                SmtpClient client = new SmtpClient("smtp.mxhichina.com", 25);//smtp.163.com，smtp.qq.com
                client.Credentials = new NetworkCredential("service@haoxilu.net", "shuaiWang1234");
                client.SendAsync(mailMsg, null);
            });
        }

    }
}
