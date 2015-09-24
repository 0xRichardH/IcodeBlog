using Abp.Application.Services.Dto;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Icode.Blog.UserInfos.Dto
{
    public class UserInfoDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(16)]
        public  string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(16)]
        public  string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [EmailAddress]
        public  string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(16)]
        [Phone]
        public  string Telphone { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [MaxLength(50)]
        public  string Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public  UserState Status { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public  bool IsAdmin { get; set; }
    }
}
