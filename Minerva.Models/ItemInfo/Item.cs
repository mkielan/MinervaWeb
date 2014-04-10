using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.ItemInfo
{
    public class Item
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public ItemType ItemType { get; set; }
    }

    public enum ItemType
    {
        Folder = 0,
        File = 1
    }
}
