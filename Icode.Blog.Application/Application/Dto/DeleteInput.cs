using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Icode.Blog.Application.Dto
{
    /// <summary>
    /// 删除用户的输入操作
    /// </summary>
    /// <typeparam name="PrimaryKey"></typeparam>
    public class DeleteInput<PrimaryKey> : IInputDto
    {
        [Required]
        public PrimaryKey Id { get; set; }

        public override string ToString()
        {
            return string.Format("[ Id={0} ]", this.Id);
        }
    }
}
