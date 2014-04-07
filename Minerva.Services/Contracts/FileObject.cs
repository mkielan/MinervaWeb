using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minevra.Services.Contracts
{
    /// <summary>
    /// Object of file to transfer via service.
    /// </summary>
    public class FileObject
    {
        public byte[] Bytes { get; set; }

        public string Type { get; set; }

        public string Extension { get; set; }
    }
}