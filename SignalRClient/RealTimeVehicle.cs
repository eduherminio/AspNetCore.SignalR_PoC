using System;

namespace SignalRClient
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

        public string Code { get; set; }

        public string Name { get; set; }

        public Position Position { get; set; }

        public int DelayStatus { get; set; }

        public double DelayTime { get; set; }

        public double PrevDelayStatus { get; set; }

        public double BackDelayStatus { get; set; }

        private readonly Random _rnd = new Random();

        public RealTimeVehicle(double lat, double lon)
        {
            AppId = _rnd.Next(1,6).ToString();
            Code = AppId;
            Name = AppId;
            DelayStatus = 0;
            DelayTime = 0;
            PrevDelayStatus = 0;
            BackDelayStatus = 0;

            Position = new Position(lat, lon);
        }
    }
}
