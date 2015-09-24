using AutoMapper;
using Icode.Blog.Categories;
using Icode.Blog.Categories.Dtos;
using Icode.Blog.Posts;
using Icode.Blog.Posts.Dtos;
using Icode.Blog.UserInfos;
using Icode.Blog.UserInfos.Dto;

namespace Icode.Blog
{
    public static class DtoMappings
    {
        public static void Map()
        {
            //分类
            Mapper.CreateMap<Category, CategoryDto>();
            
            //文章
            Mapper.CreateMap<Post, PostDto>();
            Mapper.CreateMap<Post, TopTenPostDto>();

            //用户
            Mapper.CreateMap<UserInfo, UserInfoDto>();
        }
    }
}