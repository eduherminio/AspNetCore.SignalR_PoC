using System;

namespace SignalRServer
{
    public class Position
    {
        public double Lat { get; set; }

        public double Lng { get; set; }

        public Position(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }

    public class RealTimeVehicle
    {
        public string AppId { get; set; }

        public Position Position { get; set; }


        public RealTimeVehicle(double lat, double lon)
        {
            AppId = Guid.NewGuid().ToString();
            Position = new Position(lat, lon);
        }
    }
}
