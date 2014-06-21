using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public class GenericRepository<C, E> :
        IRepository<C, E>
        where E : AbstractEntity
        where C : DbContext
    {
        private C _entities;

        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public GenericRepository(C context)
        {
            _entities = context;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual void Add(E entity)
        {
            _entities.Set<E>().Add(entity);
        }

        public virtual void Edit(E entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(E entity)
        {
            _entities.Set<E>().Remove(entity);
        }

        public virtual IQueryable<E> GetAll()
        {
            return _entities.Set<E>();
        }

        public virtual IQueryable<E> FindBy(Expression<Func<E, bool>> predicate)
        {
            return _entities.Set<E>().Where(predicate);
        }
    }
}
