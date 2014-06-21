using Microsoft.AspNet.Identity.EntityFramework;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Minerva.Entities
{
    public class MinervaDbContext : IdentityDbContext<ApplicationUser>
    {
        public MinervaDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Source> Sources { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<DiskStructure> DiskStructures { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<DiskStructure>()
                .HasOptional(ds => ds.File)
                .WithRequired(f => f.DiskStructure);
            
            modelBuilder.Entity<Tag>()
                .HasMany(ds => ds.DiskStructures);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AccessTo)
                .WithMany(ds => ds.AvailableFor);
        }
    }
}
