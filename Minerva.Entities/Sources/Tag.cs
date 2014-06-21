using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Tag : AbstractEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<DiskStructure> DiskStructures { get; set; }

        public Tag()
        {
            DiskStructures = new List<DiskStructure>();
        }
    }
}
