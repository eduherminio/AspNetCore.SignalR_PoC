using System;

namespace SignalRServer
{
    public class Vehicle
    {
        public string Code { get; set; }

        public Position Position { get; set; }

        public Vehicle(double lat, double lon)
        {
            Code = Guid.NewGuid().ToString();
            Position = new Position(lat, lon);
        }
    }
}
