using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Api
{
    public class File : FileInfo
    {
        [Required]
        public byte[] Data;
    }
}
