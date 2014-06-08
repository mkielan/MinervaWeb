using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Entities;

namespace Minerva.Repositories
{
    public abstract class GenericRepository<C, T, TId> :
        IRepository<T> where T : AbstractFkEntity<TId> where C : DbContext, new () {
    
        private C _entities = new C();
        
        public C Context {
        get { return _entities; }
        set { _entities = value; }
    }
            
        public void Save()
        {
            _entities.SaveChanges();
        }
            
        public virtual void Add(T entity)
        {
            entity.CreatedTime = DateTime.Now;
            
            _entities.Set<T>().Add(entity);
        }
        
        public virtual void Edit(T entity)
        {
            entity.ModificationTime = DateTime.Now;
            
            _entities.Entry(entity).State = EntityState.Modified;
        }
        
        public virtual void Delete(T entity)
        {
            entity.CreatedTime = DateTime.Now;

            _entities.Set<T>().Remove(entity);
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return _entities.Set<T>();
        }
            
        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T,bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate);
        }
    }
}
