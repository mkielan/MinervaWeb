using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemType = Minerva.Models.Items.ItemType;
using Info = Minerva.Models.Items.Info;
using Minerva.Entities.Sources.Internal;
using System.Configuration;

namespace Minerva.Repositories
{
    /// <summary>
    /// We wszystkich metodach będą wykorzystywane obiekty typu info. 
    /// Metoda save przewidziana tylko dla folderów.
    /// </summary>
    public class ItemsInfoRepository : IItemsRepository<Models.Items.Info, Int64>
    {
        private string _filesDirectoryPath;

        #region Constructors
        /// <summary>
        /// Directory path is reading from app.config, if it isn't exist, using application current directory.
        /// </summary>
        public ItemsInfoRepository()
        {
            Init();
        }

        public ItemsInfoRepository(string directoryPath)
        {
            if (directoryPath != null)
            {
                _filesDirectoryPath = directoryPath;
            }
            else
            {
                Init();    
            }
        }

        private void Init()
        {
            var path = ConfigurationManager.AppSettings["FilesPath"];

            if (string.IsNullOrEmpty(path))
            {
                _filesDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                _filesDirectoryPath = path;
            }
        }
        #endregion

        public IList<Info> FindByParentId(long itemId)
        {
            using (var dbContext = new MinervaDbContext())
            {
                return (from ds in dbContext.DiskStructures
                        where ds.Parent.Id == itemId
                        select new Models.Items.Info
                        {
                            Id = ds.Id,
                            ParentId = ds.Parent.Id,
                            Name = ds.Name,
                            ItemType = ds.Directory == null
                                ? Models.Items.ItemType.File
                                : Models.Items.ItemType.Folder
                        }
                        ).ToList();
            }
        }

        public IList<Info> FindAll()
        {
            using (var dbContext = new MinervaDbContext())
            {
                return (from ds in dbContext.DiskStructures
                        select new Models.Items.Info
                        {
                            Id = ds.Id,
                            ParentId = ds.Parent.Id,
                            Name = ds.Name,
                            ItemType = ds.Directory == null
                                ? Models.Items.ItemType.File
                                : Models.Items.ItemType.Folder
                        }
                        ).ToList();
            }
        }

        public Info FindById(long id)
        {
            using (var dbContext = new MinervaDbContext())
            {
                var tmp = dbContext.DiskStructures.First(f => f.Id == id);

                return new Models.Items.Info
                {
                    Id = tmp.Id,
                    Name = tmp.Name,
                    ItemType = tmp.Directory == null
                                ? Models.Items.ItemType.File
                                : Models.Items.ItemType.Folder,
                    ParentId = tmp.Parent.Id
                };
            }
        }

        public void Save(Info entity)
        {
            using (var dbContext = new MinervaDbContext())
            {
                if (entity.ItemType == ItemType.File)
                    throw new NotImplementedException();

                var item = new DiskStructure
                {
                    Parent = entity.ParentId.HasValue ? dbContext.DiskStructures.First(p => p.Id == entity.ParentId) : null,
                    Name = entity.Name,
                    Directory = new Entities.Sources.Internal.Directory
                    {
                        CreatedTime = DateTime.Now
                    }
                };

                dbContext.DiskStructures.Add(item);
            }
        }

        public void Update(Models.Items.Info entity, long id)
        {
            using (var dbContext = new MinervaDbContext())
            {
                var structure = dbContext.DiskStructures.First(ds => ds.Id == id);

                structure.Name = entity.Name;
                structure.ModificationTime = DateTime.Now;

                if (entity.ParentId.HasValue) structure.Parent = dbContext.DiskStructures.First(ds => ds.Id == entity.ParentId);
                else structure.Parent = null;

                if (entity.ItemType == ItemType.File) //File
                {
                    //todo
                }
                else // Directory
                {
                    //todo
                }

                dbContext.SaveChanges();
            }
        }

        public void Delete(Models.Items.Info entity)
        {
            using (var dbContext = new MinervaDbContext())
            {
                var toDelete = dbContext.DiskStructures.First(ds => ds.Id == entity.Id);
                var deletedTime = DateTime.Now;

                toDelete.DeletedTime = deletedTime;
                if (toDelete.Directory == null) toDelete.File.DeletedTime = deletedTime;
                else toDelete.Directory.DeletedTime = deletedTime;

                dbContext.SaveChanges();
            }
        }

        public IList<Models.Items.Info> FindMakedAvailableFor(long userId)
        {
            throw new NotImplementedException();
        }

        public IList<Models.Items.Info> FindMakedAvailableBy(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
