using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Resource
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<DiskStructure> DiskStructures { get; set; }

        public string Name { get; set; }

        public Resource()
        {
            DiskStructures = new List<DiskStructure>();
        }
    }
}
