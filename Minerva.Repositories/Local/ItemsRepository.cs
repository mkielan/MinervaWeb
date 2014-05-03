using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public class ItemsRepository : IItemsRepository<Models.Items.Item, Int64>
    {
        private MinervaDbContext _dbContext;

        public ItemsRepository(MinervaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Models.Items.Item> FindByParentId(long itemId)
        {
            throw new NotImplementedException();
        }

        public IList<Models.Items.Item> FindAll()
        {
            throw new NotImplementedException();
        }

        public Models.Items.Item FindById(long id)
        {
            throw new NotImplementedException();
        }

        public void Save(Models.Items.Item entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Items.Item entity, long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Models.Items.Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
