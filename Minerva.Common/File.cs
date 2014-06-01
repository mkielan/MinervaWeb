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
            Init(filePath);
        }
        
        public File(string dirPath, string fileName)
        {
            Init(FullNameFormat(dirPath, fileName));
        }

        private void Init(string filePath)
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

        public static string FullNameFormat(string dirPath, string filename)
        {
            return string.Format("{0}\\{1}", dirPath, filename);
        }
    }
}
