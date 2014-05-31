using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public interface IRepository<T>
    {
        void Save();

        void Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        IQueryable<T> GetAll();

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
