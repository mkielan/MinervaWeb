using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Items
{
    public class Info
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string Name { get; set; }

        public ItemType ItemType { get; set; }
    }

    public enum ItemType
    {
        Folder = 0,
        File = 1
    }
}
