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
            var item = Context.DiskStructures.First(ds => ds.Id == itemId);
            var user = Context.Users.First(u => u.UserName == username);

            var comment = new Comment
            {
                DiskStructure = item,
                CreatedBy = user,
                Body = message,
                CreatedTime = time
            };

            Context.Set<Comment>().Add(comment);
        }

        public IEnumerable<Comment> PrepareItemComments(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
