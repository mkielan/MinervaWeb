using Minerva.Entities;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public class CommentRepository : GenericRepository<MinervaDbContext, Comment, Int64>
    {
    }
}
