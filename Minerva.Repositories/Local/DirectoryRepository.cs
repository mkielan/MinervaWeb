using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories.Local
{
    /// <summary>
    /// Repozytorium zarządzania dostępem do katalogów.
    /// Katalogi reprezentowane są w strukturze tableli w bazie danych,
    /// nie są fizycznie odwzorywane na dysku.
    /// </summary>
    public class DirectoryRepository
    {
        private MinervaDbContext _context = new MinervaDbContext();

    }
}
