using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Minerva.Services.Contracts;
using System.IO;
using System.Runtime.InteropServices;

namespace Minerva.Services
{
    public class NodeService : INodeService
    {
        private string _filesDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["FilesDirectory"];

        public IList<ItemInfo> GetChildrenInfo(int? parentId)
        {
            return (from f in GetFileInfos()
                    select new ItemInfo { 
                        Name = f.Name,
                        ParentId = parentId,
                        ItemType = ItemType.File
                    }).ToList();
        }

        public void RemoveItem(int itemId)
        {
            var files = GetFileInfos();
            if (files != null && files.Count > itemId) files[itemId].Delete();
        }

        public void AddItem(int parentId, Item item)
        {
            if (Directory.Exists(_filesDirectoryPath))
            {
                try
                {
                    var fullName = _filesDirectoryPath + @"\" + item.Name;

                    if (!File.Exists(fullName))
                    {
                        File.Create(fullName);
                    }

                    // Open file for reading
                    System.IO.FileStream _FileStream =
                       new System.IO.FileStream(fullName, System.IO.FileMode.Create,
                                                System.IO.FileAccess.Write);
                    // Writes a block of bytes to this stream using data from
                    // a byte array.
                    _FileStream.Write(item.File.Bytes, 0, item.File.Bytes.Length);

                    // close file stream
                    _FileStream.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        public void ChangeItem(Item item)
        {
            throw new NotImplementedException();
        }

        public FileObject GetFile(int fileId)
        {
            var files = GetFileInfos();

            if (files != null && files.Count > fileId)
            {
                var file = files[fileId];

                var fs = file.OpenRead();
                var data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);

                if (br != fs.Length) throw new IOException(file.Name);

                #region find mime type

                string mimeType = "application/unknown";
                string ext = System.IO.Path.GetExtension(file.Name).ToLower();
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();

                #endregion

                return new FileObject
                {
                    FileName = file.Name,
                    Extension = file.Extension,
                    Bytes = data,
                    Type = mimeType
                };
            }

            return null;
        }

        public IList<ItemProperties> GetItemProperties(int itemId)
        {
            return new [] { 
                new ItemProperties {
                    Name = "Color",
                    Value = "#ffffff"
                } ,
                new ItemProperties {
                    Name = "Created by",
                    Value = "Tomasz Abacki"
                }
            };
        }

        private IList<FileInfo> GetFileInfos()
        {
            if (Directory.Exists(_filesDirectoryPath))
            {
                return (from f in Directory.GetFiles(_filesDirectoryPath)
                            select new FileInfo(f)
                            ).ToList();
            }

            return null;
        }
    }
}
