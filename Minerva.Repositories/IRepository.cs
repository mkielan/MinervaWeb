using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public interface IRepository<T, TId>
    {
        IList<T> FindAll();

        T FindById(TId id);

        void Save(T entity);

        void Update(T entity, TId id);

        void Delete(T entity);
    }
}
