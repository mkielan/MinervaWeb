using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Web.Storage
{
    
    public class GridItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public DateTime? LastModification { get; set; }

        public ItemType Type { get; set; }
    }
}
