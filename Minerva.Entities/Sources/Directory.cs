using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Directory : AbstractEntity
    {
        [Key, ForeignKey("DiskStructure")]
        public long DiskStructureId { get; set; }

        public virtual DiskStructure DiskStructure { get; set; }
    }
}
