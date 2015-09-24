using System;
using Abp.Domain.Repositories;

namespace Icode.Blog.UserInfos
{
    /// <summary>
    /// 定义用户信息的数据库操作
    /// </summary>
    public interface IUserInfoRepository : IRepository<UserInfo,long>
    {
         
    }
}