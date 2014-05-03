using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemType = Minerva.Models.Items.ItemType;

namespace Minerva.Repositories
{
    /// <summary>
    /// We wszystkich metodach będą wykorzystywane obiekty typu info. 
    /// Metoda save przewidziana tylko dla folderów.
    /// </summary>
    public class ItemsInfoRepository : IItemsRepository<Models.Items.Info, Int64>
    {
        private string _filesDirectoryPath;

        private MinervaDbContext _dbContext;

        public ItemsInfoRepository(MinervaDbContext dbContext, string directoryPath)
        {
            _dbContext = dbContext;

            if (directoryPath != null)
            {
                _filesDirectoryPath = directoryPath;
            }
            else
            {
                // todo
                //_filesDirectoryPath = zczytanie aktualnego katalogu aplikacji
            }
        }

        public IList<Models.Items.Info> FindByParentId(long itemId)
        {
            return (from ds in _dbContext.DiskStructures
                    where ds.Parent.Id == itemId
                    select new Models.Items.Info { 
                        Id = ds.Id,
                        ParentId = ds.Parent.Id,
                        Name = ds.Name,
                        ItemType = ds.Directory == null 
                            ? Models.Items.ItemType.File 
                            : Models.Items.ItemType.Folder
                    }
                    ).ToList();
        }

        public IList<Models.Items.Info> FindAll()
        {
            return (from ds in _dbContext.DiskStructures
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

        public Models.Items.Info FindById(long id)
        {
            var tmp = _dbContext.DiskStructures.First(f => f.Id == id);

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

        public void Save(Models.Items.Info entity)
        {
            if(entity.ItemType == ItemType.File)
                throw new NotImplementedException();

            //todo
        }

        public void Update(Models.Items.Info entity, long id)
        {
            var structure = _dbContext.DiskStructures.First(ds => ds.Id == id);
            
            structure.Name = entity.Name;
            structure.ModificationTime = DateTime.Now;

            if (entity.ParentId.HasValue) structure.Parent = _dbContext.DiskStructures.First(ds => ds.Id == entity.ParentId);
            else structure.Parent = null;

            if (entity.ItemType == ItemType.File) //File
            {
                //todo
            }
            else // Directory
            {
                //todo
            }

            _dbContext.SaveChanges();
        }

        public void Delete(Models.Items.Info entity)
        {
            var toDelete = _dbContext.DiskStructures.First(ds => ds.Id == entity.Id);
            var deletedTime = DateTime.Now;

            toDelete.DeletedTime = deletedTime;
            if (toDelete.Directory == null) toDelete.File.DeletedTime = deletedTime;
            else toDelete.Directory.DeletedTime = deletedTime;
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
