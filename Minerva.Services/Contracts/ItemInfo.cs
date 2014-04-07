using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Minevra.Services.Contracts
{
    [DataContract]
    public class ItemInfo
    {
        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ItemType ItemType { get; set; }
    }
}