namespace Minerva.Entities.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Minerva.Entities.Sources;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Minerva.Entities.MinervaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Minerva.Entities.MinervaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
                );

            context.DiskStructures.AddOrUpdate(ds => ds.Name,
                new DiskStructure { Name = "root", CreatedTime = DateTime.Now}
                );

        }
    }
}
