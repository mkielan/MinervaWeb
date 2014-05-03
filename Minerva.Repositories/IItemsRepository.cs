using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minerva.Models.Directory;
using Minerva.Models.File;

namespace Minerva.Repositories
{
    public interface IItemsRepository<TT, Tid> : IRepository<TT, Tid>
    {
        IList<TT> FindByParentId(Tid itemId);

        IList<TT> FindMakedAvailableFor(Tid userId);

        IList<TT> FindMakedAvailableBy(Tid userId);
    }
}
