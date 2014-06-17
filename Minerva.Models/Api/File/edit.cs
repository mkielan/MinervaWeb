using Newtonsoft.Json;
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
        [JsonProperty("name")]
        public string Name { get; set; }

        [MaxLength(400)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }
    }
}
