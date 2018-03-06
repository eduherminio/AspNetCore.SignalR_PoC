// Acting as SignalR Server
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRServer
{
    /// <summary>
    /// Shared logic for both clients (VehicleHub) and server (SignalRService), so that Hubs are only called by former ones
    /// Approach recommended in:
    /// * https://github.com/aspnet/SignalR/issues/182#issuecomment-278421834
    /// </summary>
    /// <typeparam name="THub"></typeparam>
    public class HubMethods<THub> where THub : Hub
    {
        private readonly IHubContext<THub> _hubContext;

        private readonly ConnectionManager _connectionManager;

        public HashSet<string> SignalRConnectedClients { get => _connectionManager.SignalRConnectedClients; }

        public HubMethods(IHubContext<THub> hubContext, ConnectionManager connectionManager)
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
        }

        public Task BroadcastAsync(MessageType messageType, params object[] args)
        {
            return _hubContext.Clients.All.SendAsync(messageType.ToString(), args);
        }

        public Task SendMessageAsync(MessageType messageType, string clientId, params object[] args)
        {
            return _hubContext.Clients.Client(clientId).SendAsync(messageType.ToString(), args);
        }

        public Task SendVehicleUpdate(RealTimeVehicle vehicle)
        {
            return _hubContext.Clients.All.SendAsync(MessageType.VehicleUpdate.ToString(), vehicle);
        }

        public bool IsClientConnected(string clientId)
        {
            return _connectionManager.IsClientConnected(clientId);
        }

        public void AddClient(string newClient)
        {
            _connectionManager.AddClient(newClient);
        }

        public void RemoveClient(string disconnectedClient)
        {
            _connectionManager.RemoveClient(disconnectedClient);
        }
    }
}
