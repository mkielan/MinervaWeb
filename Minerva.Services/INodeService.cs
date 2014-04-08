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
    [ServiceContract]
    public interface INodeService
    {
        [OperationContract]
        IList<ItemInfo> GetChildrenInfo(int? parentId);

        [OperationContract]
        IList<Item> GetChildren(int? parentId);

        [OperationContract]
        void RemoveItem(int itemId);

        [OperationContract]
        void AddItem(int parentId, Item item);

        [OperationContract]
        void ChangeItem(Item item);

        [OperationContract]
        IList<ItemProperties> GetProperties(int itemId);
    }
}
