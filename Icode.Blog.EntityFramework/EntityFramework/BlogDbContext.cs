using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using Icode.Blog.Categories;
using Icode.Blog.CategoryMetas;
using Icode.Blog.CommentMetas;
using Icode.Blog.Comments;
using Icode.Blog.PostMetas;
using Icode.Blog.Posts;
using Icode.Blog.UserInfos;
using Icode.Blog.UserMetas;

namespace Icode.Blog.EntityFramework
{
    public class BlogDbContext : AbpDbContext
    {
        // Define an IDbSet for each Entity...
        public virtual  IDbSet<UserInfo> UserInfo { get; set; } 

        public virtual IDbSet<UserMeta> UserMeta { get; set; } 

        public virtual  IDbSet<Category> Category { get; set; } 

        public virtual  IDbSet<CategoryMeta> CategoryMeta { get; set; } 

        public virtual IDbSet<Comment> Comment { get; set; } 

        public virtual IDbSet<CommentMeta> CommentMeta { get; set; } 

        public virtual IDbSet<Post> Post { get; set; } 

        public virtual IDbSet<PostMeta> PostMeta { get; set; } 

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public BlogDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in BlogDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of BlogDbContext since ABP automatically handles it.
         */
        public BlogDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public BlogDbContext(DbConnection connection)
            :base(connection,true)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
