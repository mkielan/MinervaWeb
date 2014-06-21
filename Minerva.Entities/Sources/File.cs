using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class File
    {
        [Key, ForeignKey("DiskStructure")]
        public int DiskStructureId { get; set; }

        [MaxLength(5)]
        public string Extension { get; set; }

        public virtual DiskStructure DiskStructure { get; set; }
    }
}
