using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Resource : AbstractFkEntity<Int64>
    {
        public virtual ICollection<DiskStructure> DiskStructures { get; set; }
    }
}
