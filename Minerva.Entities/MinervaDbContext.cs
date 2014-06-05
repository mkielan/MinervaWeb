using Microsoft.AspNet.Identity.EntityFramework;
using Minerva.Entities.Sources;
using Minerva.Entities.Sources.Internal;
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

        public DbSet<FtpSource> FtpSources { get; set; }

        public DbSet<Directory> Directories { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<DiskStructure> DiskStructures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>()
                .HasRequired(f => f.DiskStructure)
                .WithRequiredDependent();
            
            modelBuilder.Entity<Directory>()
                .HasRequired(f => f.DiskStructure)
                .WithRequiredDependent();

            modelBuilder.Entity<Source>()
                .HasOptional(f => f.FtpSource)
                .WithOptionalDependent();
        }
    }
}
