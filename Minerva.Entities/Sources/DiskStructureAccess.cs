using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class DiskStructureAccess
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        
        public virtual ApplicationUser User { get; set; }

        public ICollection<DiskStructure> DiskStructures { get; set; }

        public DiskStructureAccess()
        {
            DiskStructures = new HashSet<DiskStructure>();
        }
    }
}
