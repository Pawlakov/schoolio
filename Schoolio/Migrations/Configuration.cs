namespace Schoolio.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Schoolio.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var userNames = new[] { "student1", "student2", "student3" };

            foreach (var userName in userNames)
            {
                if (!context.Users.Any(u => u.UserName == userName))
                {
                    manager.Create(new ApplicationUser { UserName = userName }, "testtest");
                }
            }

            context.SaveChanges();
        }
    }
}
