using Microsoft.AspNet.SignalR;
using Minerva.Entities;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minerva.Infrastructure
{
    public class CommentsHub : Hub
    {
        //private ICommentRepository<MinervaDbContext> _repository = new CommentRepository();

        public void Comment(int itemId, string username, string message)
        {
            var time = DateTime.Now;
            //_repository.AddComment(itemId, username, message, time);
            //_repository.Save();

            Clients.All.addComment(username, message, time);
        }
    }
}