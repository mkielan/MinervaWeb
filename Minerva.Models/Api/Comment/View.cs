using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.Comment
{
    public class View
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("sended")]
        public DateTime Sended { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
