using System.ComponentModel.DataAnnotations;

namespace Icode.Blog.Web.Areas.AdminArea.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public string SessionId { get; set; }

        public bool RememberMe { get; set; }
    }
}