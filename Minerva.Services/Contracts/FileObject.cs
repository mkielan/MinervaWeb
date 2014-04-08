using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Minerva.Services.Contracts
{
    /// <summary>
    /// Object of file to transfer via service.
    /// </summary>
    [DataContract]
    public class FileObject
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Bytes { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Extension { get; set; }
    }
}