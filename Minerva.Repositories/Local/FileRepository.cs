using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Entities;
using Minerva.Entities.Sources;
using System.Linq.Expressions;
using CFile = Minerva.Common.File;

namespace Minerva.Repositories
{
    /// <summary>
    /// Repozytorium plików, z obsługą plików z dysku.
    /// 
    /// Pliki zapisywane są z nazwami na podstawie indentyfikatorów, bez rozszerzeń.
    /// Nazwy i rozszerzenia brane są z bazy danych.
    /// </summary>
    public class FileRepository : 
        GenericRepository<MinervaDbContext, File, Int64>
    {
        private Minerva.Common.FileStorage _storage;

        public FileRepository(string path)
        {
            _storage = new Minerva.Common.FileStorage(path);
        }

        public override void Add(File entity)
        {
            base.Add(entity);

            PutFile(entity);
        }

        public override void Delete(File entity)
        {
            base.Delete(entity);

            _storage.Delete(entity.Id.ToString());
        }

        public override void Edit(File entity)
        {
            base.Edit(entity);

            PutFile(entity);
        }

        public override IQueryable<File> GetAll()
        {
            IQueryable<File> files = base.GetAll();
            ContentComplement(ref files);

            return files;
        }

        public override IQueryable<File> FindBy(Expression<Func<File, bool>> predicate)
        {
            var files = base.FindBy(predicate);
            ContentComplement(ref files);

            return files;
        }

        /// <summary>
        /// uzupełnienie o zawartość pliku
        /// </summary>
        /// <param name="files"></param>
        private void ContentComplement(ref IQueryable<File> files)
        {
            foreach (var file in files)
            {
                var f = _storage.Get(file.Id.ToString());
                file.Body = f.Content;
            }
        }

        private void ContentComplement(ref File file)
        {
            var f = _storage.Get(file.Id.ToString());
            file.Body = f.Content;
        }

        /// <summary>
        /// Odłożenie pliku
        /// </summary>
        /// <param name="entity"></param>
        private void PutFile(File entity)
        {
            _storage.Save(
                new CFile
                {
                    Name = entity.Id.ToString(),
                    Content = entity.Body
                });
        }
    }
}
