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
}
