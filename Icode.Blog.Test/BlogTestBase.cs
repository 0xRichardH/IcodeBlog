using System;
using System.Data.Common;
using Abp.Collections;
using Abp.Modules;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using Icode.Blog.EntityFramework;
using Icode.Blog.Test.InitialData;

namespace Icode.Blog.Test
{
    public abstract class BlogTestBase : AbpIntegratedTestBase
    {
        protected BlogTestBase()
        {
            //Fact DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                      .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                      .LifestyleSingleton()
                );

            //Seed initial data
            UsingDbContext(context => new BlogInitialDataBuilder().Build(context));
        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);

            //Adding testing modules. Depended modules of these modules are automatically added.
            modules.Add<BlogApplicationModule>();
            modules.Add<BlogDataModule>();

        }

        public void UsingDbContext(Action<BlogDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<BlogDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<BlogDbContext,T> func )
        {
            T result;
            using (var context = LocalIocManager.Resolve<BlogDbContext>())
            {
                result = func.Invoke(context);
                context.SaveChanges();
            }
            return result;
        }
    }
}