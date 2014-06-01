using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOFile = System.IO.File;

namespace Minerva.Common
{
    /// <summary>
    /// Helper do zarządzanie przestrzenią dyskową.
    /// </summary>
    public class FileStorage
    {
        private string _directoryPath;

        public string DirecoryPath { get { return _directoryPath; } }

        public FileStorage(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public File Get(string fileName)
        {
            return new File(string.Format("{0}/{1}", _directoryPath, fileName));
        }

        public IQueryable<File> Get(string[] fileNames)
        {
            var list = new List<File>();

            foreach (var fileName in fileNames)
            {
                list.Add(Get(fileName));
            }

            return list.AsQueryable<File>();
        }

        public void Save(File file)
        {
            file.Save(_directoryPath);
        }

        public bool Delete(File file)
        {
            return Delete(file.Name);
        }

        public bool Delete(string fileName)
        {
            if (IOFile.Exists(fileName))
            {
                IOFile.Delete(fileName);
            }

            return true;
        }
    }
}
