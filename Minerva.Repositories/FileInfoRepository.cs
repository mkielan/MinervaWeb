using Minerva.Entities;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    /// <summary>
    /// Daje jedynie dostęp do informacji i pliku. Nie daje możliwości pracy z zawartością pliku.
    /// </summary>
    public class FileInfoRepository : GenericRepository<MinervaDbContext, File, Int64>
    {
    }
}
