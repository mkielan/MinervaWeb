using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources.Internal
{
    public class DiskStructure : AbstractFkEntity<Int64>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Path { get; set; }

        [Column("Parent")]
        public DiskStructure Parent { get; set; }
        
        public virtual Directory Directory { get; set; }

        public virtual File File { get; set; }

        public virtual ICollection<DiskStructure> Children { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
