using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR.RabbitMq.Example
{
    public class Chat : Hub
    {
        private static int _id;
        public void Send(string message)
        {
            Clients.All.addMessage($"{message} - {_id}", Context.ConnectionId);
            Clients.Others.addMessage($"Some one said - {message} - {_id}", Context.ConnectionId);
            _id++;
        }

        private const string GroupName = "SecretGroup";
        public void SendGroup(string message)
        {
            Clients.Group(GroupName).addMessage($"{message} - {_id}", GroupName);
            _id++;
        }

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, GroupName);
        }
    }
}
