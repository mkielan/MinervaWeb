using Minerva.Entities;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public class CommentRepository : GenericFullRepository<MinervaDbContext, Comment>,
        ICommentRepository<MinervaDbContext>
    {
        public CommentRepository(MinervaDbContext context)
            : base(context)
        {
        }

        public void AddComment(int itemId, string username, string message, DateTime time)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> PrepareItemComments(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
