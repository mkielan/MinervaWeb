using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Tag : AbstractFkEntity<Int64>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<DiskStructure> DiskStructures { get; set; }
    }
}
