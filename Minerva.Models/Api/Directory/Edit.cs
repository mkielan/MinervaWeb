using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.Directory
{
    public class Edit
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }
    }
}
