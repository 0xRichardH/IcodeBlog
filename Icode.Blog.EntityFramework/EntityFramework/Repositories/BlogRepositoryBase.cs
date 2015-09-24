using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Icode.Blog.EntityFramework.Repositories
{
    public abstract class BlogRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BlogDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BlogRepositoryBase<TEntity> : BlogRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BlogRepositoryBase(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
