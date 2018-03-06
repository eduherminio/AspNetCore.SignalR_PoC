using System;
using System.Threading;

using SignalRServer;

namespace SignalRServer
{
    public class PositionGenerator
    {

        private static PositionGenerator _instance = new PositionGenerator();

         
        private readonly Random _rnd = new Random();
        private ISignalRService _signalRService;
        private int _invokeCount;

        public static PositionGenerator Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Config(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        private PositionGenerator()
        {
            var autoEvent = new AutoResetEvent(false);
            var stateTimer = new Timer(CheckStatus, autoEvent, 1000, 250);
        }

        public void CheckStatus(object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            Console.WriteLine("{0} Checking status {1,2}.",
                DateTime.Now.ToString("h:mm:ss.fff"),
                (++_invokeCount).ToString());   

            double lat = 0.0001 * _rnd.Next(416000, 417000);
            double lon = 0.0001 * _rnd.Next(-48000, -46000);
            System.Diagnostics.Debug.WriteLine("New position:  ({0}, {1})", lat, lon);

            _signalRService.UpdateVehicle(new RealTimeVehicle(lat, lon));

            if ((int)(100 * lat) % 5 == 0 && (int)(100 * lon) % 5 == 0)
                _signalRService.NotifyConnectionIssues(string.Empty);
            else if ((int)(100 * lat) % 5 == 0)
                _signalRService.NotifyDowntime();
            else if ((int)(100 * lon) % 10 == 0)
                _signalRService.NotifyNewVersion();
        }
    }
}
