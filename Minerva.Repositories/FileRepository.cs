using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Entities;
using Minerva.Entities.Sources.Internal;
using System.Linq.Expressions;

namespace Minerva.Repositories
{
    /// <summary>
    /// Repozytorium plików, z obsługą plików z dysku.
    /// </summary>
    public class FileRepository : 
        GenericRepository<MinervaDbContext, File, Int64>
    {
        public override void Add(File entity)
        {

            base.Add(entity);
        }

        public override void Delete(File entity)
        {
            base.Delete(entity);
        }

        public override void Edit(File entity)
        {
            base.Edit(entity);
        }

        public override IQueryable<File> GetAll()
        {
            return base.GetAll();
        }

        public override IQueryable<File> FindBy(Expression<Func<File, bool>> predicate)
        {
            return base.FindBy(predicate);
        }
    }
}
