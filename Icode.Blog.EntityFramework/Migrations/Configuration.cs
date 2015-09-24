using System.Data.Entity.Migrations;

namespace Icode.Blog.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Blog.EntityFramework.BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Blog";
        }

        protected override void Seed(Blog.EntityFramework.BlogDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
