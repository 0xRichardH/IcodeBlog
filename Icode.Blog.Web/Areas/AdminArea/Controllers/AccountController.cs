using Abp.Net.Mail;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Models;
using Icode.Blog.UserInfos;
using Icode.Blog.Web.Areas.AdminArea.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Icode.Blog.Web.Areas.AdminArea.Controllers
{
    /// <summary>
    /// 账户操作
    /// </summary>
    public class AccountController : AbpController
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private readonly IUserInfoService _userInfoService;
        private readonly IEmailSender _emailSender;
        public AccountController(IUserInfoService userInfoService, IEmailSender emailSender)
        {
            this._userInfoService = userInfoService;
            this._emailSender = emailSender;
        }

        /// <summary>
        /// 显示登陆页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.SessionId = Guid.NewGuid();
            ViewBag.Host = Request.UserHostAddress;

            return View();
        }

        /// <summary>
        /// 邮件发送操作
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SendEmail(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new MvcAjaxResponse { Success = false, Result = "校验失败" });
            }

            //记录尝试获取验证码的邮箱
            Logger.InfoFormat("用户ip：{0},邮箱：{1} 尝试获取验证码",
                Request.UserHostAddress,
                loginModel.Email
                );

            //校验用户邮箱是否存在
            var user = await this._userInfoService.GetUserInfoByEmail(new UserInfos.Dto.UserInfoInput
            {
                Email = loginModel.Email
            });
            if (user.UserInfo == null || !user.UserInfo.IsAdmin)
            {
                return Json(new MvcAjaxResponse { Success = false, Result = "邮箱不存在" });
            }

            var output = await this._userInfoService.GenderValidateCode(new UserInfos.Dto.UserValidateCodeInput
            {
                NameOrEmail = loginModel.Email,
                HostUrl = loginModel.Host,
                SessionId = loginModel.SessionId
            });
            string vcode = output.ValidateCode;//验证码

            //await this._emailSender.SendAsync(loginModel.Email, "邮箱登陆验证码", vcode);
            await this._userInfoService.SendEmailAsync(loginModel.Email, "邮箱登陆验证码", vcode);

            //记录日志
            Logger.InfoFormat("用户ip：{0},邮箱：{1},验证码：{2},状态:发送成功",
                            Request.UserHostAddress,
                            loginModel.Email, vcode);

            return Json(new MvcAjaxResponse { Success = true });
        }

        /// <summary>
        /// 登陆操作
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Your form is invalid!");
            }

            var userInfo = await this._userInfoService.GetUserInfoByEmail(new UserInfos.Dto.UserInfoInput { Email = loginModel.Email });

            //用户信息为空，或用户不是管理员
            if (userInfo.UserInfo == null || !userInfo.UserInfo.IsAdmin)
            {
                throw new UserFriendlyException("验证码无效");
            }

            //校验验证码是否有效
            bool checkResult = await this._userInfoService.CheckValidateCode(new UserInfos.Dto.UserValidateCodeInput
            {
                NameOrEmail = loginModel.Email,
                HostUrl = loginModel.Host,
                SessionId = loginModel.SessionId,
                VCode = loginModel.ValidateCode
            });

            if (!checkResult)
            {
                //验证未通过
                throw new UserFriendlyException("验证码无效");
            }
            var claimsIdentity = this._userInfoService.CreateIdentity(userInfo.UserInfo, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = loginModel.RememberMe }, claimsIdentity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            //登陆成功
            Logger.InfoFormat("用户ip：{0},邮箱：{1},DNS：{2},验证码：{3}，登陆后台成功!",
                                Request.UserHostAddress,
                                loginModel.Email,
                                Request.UserHostName,
                                loginModel.ValidateCode);

            return Json(new MvcAjaxResponse { TargetUrl = returnUrl });
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("login");
        }
    }
}