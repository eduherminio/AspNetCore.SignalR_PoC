using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

using SignalRServer;

namespace SignalRServer
{
    /// <summary>
    /// Methods to be executed by clients
    /// </summary>
    public class VehicleHub : Hub
    {
        private readonly HubMethods<VehicleHub> _hubMethods;

        public VehicleHub(HubMethods<VehicleHub> hubMethods)
        {
            _hubMethods = hubMethods;
        }

        public override Task OnConnectedAsync()
        {
            _hubMethods.AddClient(Context.ConnectionId);
            Clients.Others.SendAsync("MemberJoined", $"{Context.ConnectionId} joined");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            _hubMethods.RemoveClient(Context.ConnectionId);
            Clients.Others.SendAsync("MemberLeft", $"{Context.ConnectionId} left");
            return base.OnDisconnectedAsync(e);
        }

        public Task ClientMessageAsync(string message)
        {
            System.Diagnostics.Debug.WriteLine("Message received from a client");
            return _hubMethods.BroadcastAsync(MessageType.ServerBroadcast, "Member " + Context.ConnectionId + "want to share something: " + message);
        }
    }
}
