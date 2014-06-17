using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.File
{
    public class Add
    {
        [Required]
        [MaxLength(200)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public int ParentId { get; set; }

        [MaxLength(200)]
        [JsonProperty("description")]
        public string Description { get; set;}
    }
}
