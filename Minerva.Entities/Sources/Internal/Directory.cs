using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources.Internal
{
    public class Directory : AbstractFkEntity<Int64>
    {
        public DiskStructure DiskStructure { get; set; }
    }
}
