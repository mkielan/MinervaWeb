using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Minerva.Repositories
{
    /// <summary>
    /// Repozytorium zarządzania dostępem do plików.
    /// Wykorzystywana zarwno baza danych jak i system plików.
    /// </summary>
    public class FileRepository : IRepository<Models.File.Item, Int64>
    {
        private MinervaDbContext _dbContext;

        public FileRepository(MinervaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Add(Models.File.Item entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Models.File.Item entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Models.File.Item entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.File.Item> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.File.Item> FindBy(Expression<Func<Models.File.Item, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
