using System;
using System.Data.Entity.Migrations;
using Icode.Blog.Categories;
using Icode.Blog.EntityFramework;
using Icode.Blog.UserInfos;

namespace Icode.Blog.Test.InitialData
{
    /// <summary>
    /// 初始化需要测试的数据
    /// </summary>
    public class BlogInitialDataBuilder
    {
        public void Build(BlogDbContext context)
        {
            //initial userinfo data
            var userInfo = new UserInfo()
            {
                NickName = "icode",
                UserName = "auto_icode",
                Telphone = "15689725090",
                Email = "mail@haoxilu.net",
                IsAdmin = true
            };

            context.UserInfo.AddOrUpdate(userInfo);

            context.SaveChanges();

            //initial Category Data
            context.Category.AddOrUpdate(new Category()
             {
                 Id =Guid.Parse("81f79e33-e179-4038-bbb5-1a90be081ca9"),
                 CategoryName = "文章",
                 Description = "用来发布文章的",
                 Order = 0,
                 Count = 0,
                 CreatorUser =  userInfo
             });

            context.SaveChanges();
        }
    }
}