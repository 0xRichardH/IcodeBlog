using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Icode.Blog.UserInfos;

namespace Icode.Blog.UserMetas
{
    /// <summary>
    /// 用户信息拓展表
    /// </summary>
    public class UserMeta : FullAuditedEntity<Guid,UserInfo>
    {
        /// <summary>
        /// 对应的用户
        /// </summary>
        [Required]
        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }

        /// <summary>
        /// 对应的用户Id
        /// </summary>
        public virtual  long? UserId { get; set; }

        /// <summary>
        /// 键名
        /// </summary>
        [Required]
        public virtual string MetaKey { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        public virtual string MetaValue { get; set; }
    }
}