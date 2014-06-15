using Microsoft.AspNet.Identity.EntityFramework;
using Minerva.Entities.Sources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Minerva.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public virtual ICollection<DiskStructure> AccessTo { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}