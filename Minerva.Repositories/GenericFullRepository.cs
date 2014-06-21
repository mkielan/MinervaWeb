using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Entities;
using System.Linq.Expressions;

namespace Minerva.Repositories
{
    public abstract class GenericFullRepository<C, T> :
        GenericRepository<C,T> where T : AbstractEntity where C : DbContext {

        protected GenericFullRepository(C context) 
            : base(context)
        {
        }

        public virtual void Add(T entity)
        {
            entity.CreatedTime = DateTime.Now;

            base.Add(entity);
            Context.Set<T>().Add(entity);
        }
        
        public virtual void Edit(T entity)
        {
            entity.ModificationTime = DateTime.Now;

            base.Edit(entity);
        }
        
        public virtual void Remove(T entity)
        {
            entity.DeletedTime = DateTime.Now;

            base.Edit(entity);
        }
    }
}
