using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Web.Comment
{
    public class Item
    {
        public string Author { get; set; }

        public string Body { get; set; }

        public DateTime SendTime { get; set; }
    }
}
