using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api.Comment
{
    public class View
    {
        public long Id { get; set; }

        public string Body { get; set; }

        public DateTime Sended { get; set; }

        public string Username { get; set; }
    }
}
