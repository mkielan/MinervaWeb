using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Entities.Sources.Internal
{
    public enum ItemType : int
    {
        File,
        Directory
    }

    public class Item : AbstractEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Item Parent { get; set; }

        [Required]
        public string PhysicalPath { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ItemType ItemType { get; set; }

        [Required]
        public Source Source { get; set; }
    }
}
