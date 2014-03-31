using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Minevra.Entities
{
    public class MinevraDbContext : IdentityDbContext<ApplicationUser>
    {
        public MinevraDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
