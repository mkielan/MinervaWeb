using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOFile = System.IO.File;
using IODir = System.IO.Directory;

namespace Minerva.Common
{
    /// <summary>
    /// Helper do zarządzanie przestrzenią dyskową.
    /// </summary>
    public class FileStorage
    {
        private string _directoryPath;

        public string DirecoryPath { get { return _directoryPath; } }

        public int Count
        {
            get
            {
                return IODir.GetFiles(_directoryPath).Length;
            }
        }

        public FileStorage(string directoryPath)
        {
            _directoryPath = directoryPath;
            if (!IODir.Exists(_directoryPath))
            {
                IODir.CreateDirectory(_directoryPath);
            }
        }

        public File Get(string fileName)
        {
            return new File(_directoryPath, fileName);
        }

        public ICollection<File> Get(string[] fileNames)
        {
            var list = new List<File>();

            foreach (var fileName in fileNames)
            {
                list.Add(Get(fileName));
            }

            return list;
        }

        public void Save(File file)
        {
            file.Save(_directoryPath);
        }

        public void Save(File[] files)
        {
            foreach (var f in files)
            {
                Save(f);
            }
        }

        public bool Delete(File file)
        {
            return Delete(file.Name);
        }

        public bool Delete(string fileName)
        {
            if (IOFile.Exists(File.FullNameFormat(_directoryPath,fileName)))
            {
                IOFile.Delete(File.FullNameFormat(_directoryPath, fileName));
            }

            return true;
        }
    }
}
