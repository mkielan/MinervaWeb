using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Minerva.Entities.Sources
{
    public class Source
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<ApplicationUser> SharedWith { get; set; }
        
        public ICollection<DiskStructure> DiskStructure { get;set; }
    }
}
