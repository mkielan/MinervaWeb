using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities
{
    public class AbstractEntity
    {
        public DateTime CreatedTime { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public DateTime? ModificationTime { get; set; }

        public virtual ApplicationUser ModifiedBy { get; set; }

        public DateTime? DeletedTime { get; set; }

        public virtual ApplicationUser DeletedBy { get; set; }
    }
}
