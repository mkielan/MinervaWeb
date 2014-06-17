using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.File
{
    public class View
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
        
        /// <summary>
        /// Rozszerzenie
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        /// <summary>
        /// Ostatnia modyfikująca osoba
        /// </summary>
        [JsonProperty("last_modification")]
        public string LastModificator { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }
    }
}
