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

        public ICollection<Comment> Comments { get; set; }

        public ICollection<DiskStructureAccess> AvailabolFor { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public DiskStructure()
        {
            //AvailableFor = new List<ApplicationUser>();
            AvailabolFor = new HashSet<DiskStructureAccess>();
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            Resources = new HashSet<Resource>();
        }
    }
}
