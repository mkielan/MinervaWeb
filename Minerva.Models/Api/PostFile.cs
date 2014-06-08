using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api
{
    public class PostFile
    {
        [Required]
        public string Body { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public int? ParentId { get; set; }

    }
}
