using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Models.Drive
{
    public class DriveFile
    {
        public string title { get; set; }
        public string description { get; set; }
        public string mimeType { get; set; }
        public string content { get; set; }
        /*
        public DriveFile(File file, string content)
        {
            this.title = file.Title;
            this.description = file.Description;
            this.mimeType = file.MimeType;
            this.content = content;
        }*/
    }
}
