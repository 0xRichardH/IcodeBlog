using System.Reflection;
using Abp.Modules;
using Icode.Blog.Authorization;

namespace Icode.Blog
{
    [DependsOn(typeof(BlogCoreModule))]
    public class BlogApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Authorization.Providers.Add<BlogAuthorizationProvider>();

            //在使用AutoMapper之前先定义
            DtoMappings.Map();
        }
    }
}
