using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Minerva.Hubs
{
    public class CommentsHub : Hub
    {
        public void Send(string name, int itemId, string msg)
        {
            var time = DateTime.Now;
            
            Clients.Group("item-" + itemId).addNewMessage(name, msg, time.ToString("f"));
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