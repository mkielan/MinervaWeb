using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class DiskStructure : AbstractFkEntity<Int64>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        /*
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }
        */
        public DiskStructure Parent { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        public virtual Directory Directory { get; set; }
        
        public virtual File File { get; set; }

        public virtual ICollection<DiskStructure> Children { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> AvailableFor { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
