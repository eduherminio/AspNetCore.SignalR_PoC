using System.Collections.Generic;

namespace SignalRServer
{
    /// <summary>
    /// Sample class to test sending messages to specific client
    /// </summary>
    public class ConnectionManager
    {
        public HashSet<string> SignalRConnectedClients { get; set; }

        public ConnectionManager()
        {
            SignalRConnectedClients = new HashSet<string>();
        }

        public bool IsClientConnected(string clientId)
        {
            string str = string.Empty;
            return SignalRConnectedClients.TryGetValue(clientId, out str);
        }

        public void AddClient(string newClient)
        {
            SignalRConnectedClients.Add(newClient);
        }

        public void RemoveClient(string disconnectedClient)
        {
            if (IsClientConnected(disconnectedClient))
                SignalRConnectedClients.Remove(disconnectedClient);
        }
    }
}
