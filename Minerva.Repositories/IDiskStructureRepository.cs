using Minerva.Entities;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public enum DiskItemType
    {
        Directory,
        File,
        All
    }

    public enum PrivilageToDiskItemType
    {
        Owner,
        Shared
    }

    public interface IDiskStructureRepository<C> 
        : IRepository<C, DiskStructure>
    {
        /*
        /// <summary>
        /// Udostępnia zasób na dysku wskazanemu userowi. Razem ze wszystkimi jego potomkami.
        /// </summary>
        /// <param name="diskItemId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool ShareWith(DiskStructure entity, string username);

        IEnumerable<DiskStructure> GetFiles(string username);

        IEnumerable<DiskStructure> GetFiles(string username, int parentId);

        IEnumerable<DiskStructure> GetDirectories(string username);

        IEnumerable<DiskStructure> GetDirectories(string username, int parentId);

        IEnumerable<DiskStructure> FindShared(string username, DiskItemType type = DiskItemType.All, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner);

        IEnumerable<DiskStructure> FindByTag(string tag, DiskItemType type = DiskItemType.All, string username = null, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner);*/
    }
}
