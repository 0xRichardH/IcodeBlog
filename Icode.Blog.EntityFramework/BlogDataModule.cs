using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using Icode.Blog.EntityFramework;

namespace Icode.Blog
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(BlogCoreModule))]
    public class BlogDataModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //Database.SetInitializer<BlogDbContext>(null);
        }
    }
}
