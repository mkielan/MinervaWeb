using Minerva.Entities.Sources.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Minerva.Entities.Sources
{
    public class Source : AbstractEntity
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
        public ApplicationUser Owner { get; set; }

        public virtual ICollection<ApplicationUser> SharedWith { get; set; }

        public InternalSource InternalSource { get; set; }

        public FtpSource FtpSource { get; set; }
        
        public virtual ICollection<Directory> Directories { get;set; }
    }
}
