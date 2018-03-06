namespace SignalRServer
{
    public interface ISignalRService
    {
        void UpdateVehicle(RealTimeVehicle realTimeVehicle);

        void NotifyDowntime();

        void NotifyNewVersion();

        void NotifyConnectionIssues(string connectionIdentifier);
    }
}
