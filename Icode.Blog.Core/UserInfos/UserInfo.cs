using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Icode.Blog.UserInfos
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : FullAuditedEntity<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(16)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(16)]
        public virtual string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [EmailAddress]
        public virtual string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(16)]
        [Phone]
        public virtual string Telphone { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [MaxLength(50)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public virtual  UserState Status { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public virtual  bool IsAdmin { get; set; }


        public UserInfo()
        {
            //用户的状态默认为启用
            this.Status = UserState.Enable;
        }
    }
}