using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class Comment : AbstractEntity<Int64>
    {
        [MaxLength(500)]
        public string Body { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Column("Author")]
        public ApplicationUser Author { get; set; }
    }
}
