using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities
{
    public abstract class AbstractEntity<T>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public DateTime? ModificationTime { get; set; }

        public ApplicationUser ModifiedBy { get; set; }

        public DateTime? DeletedTime { get; set; }

        public ApplicationUser DeletedBy { get; set; }
    }
}
