using System.Reflection;
using Abp.Modules;

namespace Icode.Blog
{
    public class BlogCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
