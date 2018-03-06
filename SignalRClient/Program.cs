using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    static internal class Program
    {
        static private string _url = "http://localhost:6666/RealTimeVehicles/";

        static private HubConnection _connection;

        static void Main(string[] args)
        {
            StartConnectionAsync();
            try
            {
                _connection.On<RealTimeVehicle>("VehicleUpdate", vehicle =>
                {
                    Console.WriteLine(/*vehicle.AppId +*/ ": [" + vehicle.Position.Lat + ", " + vehicle.Position.Lng + "]");
                });
                _connection.On<string>("ServerBroadcast", message =>
                {
                    Console.WriteLine("[BROADCAST]: " + message);
                });
                _connection.On<string>("ServerMessage", message =>
                {
                    Console.WriteLine("[INDIVIDUAL MESSAGE] : message");
                    _connection.InvokeAsync("ClientMessageAsync", "Hi there!");
                });
                _connection.On<string>("MemberJoined", message =>
                {
                    Console.WriteLine(message);
                });
                _connection.On<string>("MemberLeft", message =>
                {
                    Console.WriteLine(message);
                });
                Console.ReadLine();
                DisposeAsync();
            }
            catch
            {
                StartConnectionAsync();
            }
        }

        public static Task StartConnectionAsync()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(_url)
                 .WithConsoleLogger()
                 .Build();

            return _connection.StartAsync(); ;
        }

        public static Task DisposeAsync()
        {
            return _connection.DisposeAsync();
        }
    }
}