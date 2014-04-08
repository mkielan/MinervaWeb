using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Minerva.Services.Contracts;

namespace Minerva.Services
{
    public class NodeService : INodeService
    {
        public IList<ItemInfo> GetChildrenInfo(int? parentId)
        {
            throw new NotImplementedException();
        }

        public IList<Item> GetChildren(int? parentId)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public void AddItem(int parentId, Item item)
        {
            throw new NotImplementedException();
        }

        public void ChangeItem(Item item)
        {
            throw new NotImplementedException();
        }

        public IList<ItemProperties> GetProperties(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
