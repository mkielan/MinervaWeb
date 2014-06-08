using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api
{
    public class FileInfo : StorageItem
    {
        [MaxLength(200)]
        public string Extension { get; set; }

        [MaxLength(255)]
        public string MimeType { get; set; }
    }
}
