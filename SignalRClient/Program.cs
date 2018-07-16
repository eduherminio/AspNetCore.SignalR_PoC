using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    static internal class Program
    {
        private const string _defaultUrl = "http://localhost:6666/Vehicles/";

        static private HubConnection _connection;

        static void Main(string[] args)
        {
            string url = args.Length > 0 ? args[0] : _defaultUrl;

            StartConnectionAsync(url);
            try
            {
                _connection.On<Vehicle>(MessageType.VehicleUpdate.ToString(), vehicle =>
                {
                    Console.WriteLine(vehicle.Code + ": [" + vehicle.Position.Lat + ", " + vehicle.Position.Lng + "]");
                });
                _connection.On<string>(MessageType.ServerBroadcast.ToString(), message =>
                {
                    Console.WriteLine("[BROADCAST]: " + message);
                });
                _connection.On<string>(MessageType.ServerMessage.ToString(), message =>
                {
                    Console.WriteLine("[INDIVIDUAL MESSAGE] : message");
                    _connection.InvokeAsync("ClientMessageAsync", "Hi there!");
                });
                _connection.On<string>(MessageType.MemberJoined.ToString(), message =>
                {
                    Console.WriteLine(message);
                });
                _connection.On<string>(MessageType.MemberLeft.ToString(), message =>
                {
                    Console.WriteLine(message);
                });
                Console.ReadLine();
                DisposeAsync();
            }
            catch
            {
                StartConnectionAsync(url);
            }
        }

        public static Task StartConnectionAsync(string url)
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .ConfigureLogging(logging =>
                 {
                     logging.AddConsole();
                 })
                 .Build();

            return _connection.StartAsync(); ;
        }

        public static Task DisposeAsync()
        {
            return _connection.DisposeAsync();
        }
    }
}