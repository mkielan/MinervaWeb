using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Minerva.Services.Contracts
{
    [DataContract]
    public class Item : ItemInfo
    {
        [DataMember]
        public FileObject File { get; set; }
    }
}