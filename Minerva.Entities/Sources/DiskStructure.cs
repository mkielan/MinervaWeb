using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources
{
    public class DiskStructure : AbstractEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual DiskStructure Parent { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }
        
        public virtual File File { get; set; }

        //public virtual ICollection<DiskStructure> Children { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUser> AvailableFor { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Resource Icon { get; set; }

        public DiskStructure()
        {
            Comments = new List<Comment>();
            AvailableFor = new List<ApplicationUser>();
            Tags = new List<Tag>();
        }
    }
}
