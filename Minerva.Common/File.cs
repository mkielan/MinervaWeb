using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOFile = System.IO.File;
using IODir = System.IO.Directory;

namespace Minerva.Common
{
    public class File
    {
        public string Name { get; set; }
        
        /// <summary>
        /// Byte body of file
        /// </summary>
        public byte[] Content { get; set; }

        public File()
        {

        }

        public File(string filePath)
        {
            if (!IOFile.Exists(filePath))
            {
                throw new ArgumentException("Select file not exist");
            }

            // zczytanie pliku
            Name = filePath.Split('\\').Last();
            Content = IOFile.ReadAllBytes(filePath);
        }

        public void Save(string directoryPath)
        {
            if(IODir.Exists(directoryPath))
                IOFile.WriteAllBytes(directoryPath + "/" + Name, Content);
        }
    }
}
