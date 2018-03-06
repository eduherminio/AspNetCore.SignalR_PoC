using SignalRServer;
using System;
using System.Linq;

namespace SignalRServer
{
    public class SignalRService : ISignalRService
    {
        HubMethods<VehicleHub> _hubMethods { get; set; }

        private readonly Random _rnd = new Random();

        public SignalRService(HubMethods<VehicleHub> hubMethods)
        {
            _hubMethods = hubMethods;
        }

        public void UpdateVehicle(RealTimeVehicle realTimeVehicle)
        {
            _hubMethods.SendVehicleUpdate(realTimeVehicle);
        }

        public void NotifyDowntime()
        {
            _hubMethods.BroadcastAsync(MessageType.ServerBroadcast, "Downtime expected during next few minutes due to server maintanance");
        }

        public void NotifyNewVersion()
        {
            _hubMethods.BroadcastAsync(MessageType.ServerBroadcast, "New version available");
        }

        public void NotifyConnectionIssues(string connectionIdentifier)
        {
            string client = connectionIdentifier;
            if (string.IsNullOrEmpty(connectionIdentifier) && _hubMethods.SignalRConnectedClients.Any())
                client = _hubMethods.SignalRConnectedClients.ElementAt(_rnd.Next(0, _hubMethods.SignalRConnectedClients.Count - 1));

            _hubMethods.SendMessageAsync(MessageType.ServerMessage, client, "There seem to exist issues with your conexion, behaviour may change unexpectedly");
        }
    }
}
