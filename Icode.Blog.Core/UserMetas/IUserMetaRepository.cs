using System;
using Abp.Domain.Repositories;

namespace Icode.Blog.UserMetas
{
    /// <summary>
    /// 定义用户拓展类数据库操作
    /// </summary>
    public interface IUserMetaRepository : IRepository<UserMeta,Guid>
    {
         
    }
}