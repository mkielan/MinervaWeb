using Microsoft.AspNet.SignalR;
using Minerva.Entities;
using Minerva.Entities.Sources;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Minerva.Hubs
{
    public class CommentsHub : Hub
    {
        private ICommentRepository<MinervaDbContext> _repository;

        public CommentsHub()
        {
            _repository = new CommentRepository(new MinervaDbContext());
        }

        public void Send(string name, int itemId, string msg)
        {
            try
            {
                var item = _repository.Context.DiskStructures.First(ds => ds.Id == itemId);
                var user = _repository.Context.Users.First(u => u.UserName == name);
                var time = DateTime.Now;

                var comment = new Comment
                {
                    DiskStructure = item,
                    CreatedBy = user,
                    Body = msg,
                    CreatedTime = time
                };

                _repository.Context.Set<Comment>().Add(comment);
                _repository.Save();

                // rozgłaszana tylko, gdy przy wkładaniu do repozytorium nie zdarzy się wyjątek
                Clients
                    .Group("item-" + itemId)
                    .addNewMessage(name, msg, time.ToString("f"));
            }
            catch(Exception){
                // todo wywołanie metody klienta o poinformowaniu o błędzie
            }
        }
        
        public Task JoinItemGroup(int itemId)
        {
            return JoinGroup("item-" + itemId);
        }

        public Task LeaveItemGroup(int itemId)
        {
            return LeaveGroup("item-" + itemId);
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}