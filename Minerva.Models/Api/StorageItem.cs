using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api
{
    public abstract class StorageItem
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public long ParentId { get; set; }

        [MaxLength(500)]
        public string Path { get; set; }
    }
}
