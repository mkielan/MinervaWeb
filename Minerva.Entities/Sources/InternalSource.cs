using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class InternalSource : AbstractEntity
    {
        [Required]
        public virtual Source Source { get; set; }
    }
}
