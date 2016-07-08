
using System;
using Microsoft.AspNet.SignalR;

namespace SignalR.RabbitMq.Console
{
    public class Chat : Hub
    {
        private static int _id;
        public void Send(string message)
        {
            Clients.All.addMessage($"{message} - {_id++}", Context.ConnectionId);
            Clients.Others.addMessage($"Some one said - {message} - {_id++}", Context.ConnectionId);
        }
    }
}
