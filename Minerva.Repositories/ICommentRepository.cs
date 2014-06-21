using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.Repositories
{
    public interface ICommentRepository<C> 
        : IRepository<C, Comment>
    {
        void AddComment(int itemId, string username, string message, DateTime time);

        IEnumerable<Comment> PrepareItemComments(int itemId);
    }
}
