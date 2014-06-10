using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.File
{
    public class Edit
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

        public string[] Tags { get; set; }
    }
}
