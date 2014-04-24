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

        public DbSet<InternalSource> InternalSources { get; set; }

        public DbSet<FtpSource> FtpSources { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}
