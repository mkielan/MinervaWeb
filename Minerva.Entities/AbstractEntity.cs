using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            CreatedTime = DateTime.Now;
        }

        public DateTime CreatedTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        public DateTime? DeletedTime { get; set; }
    }
}
