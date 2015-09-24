using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Icode.Blog
{
    [DependsOn(typeof(AbpWebApiModule), typeof(BlogApplicationModule))]
    public class BlogWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(BlogApplicationModule).Assembly, "blog")
                .Build();
        }
    }
}
