namespace SignalRServer
{
    public interface ISignalRService
    {
        void UpdateVehicle(Vehicle Vehicle);

        void NotifyDowntime();

        void NotifyNewVersion();

        void NotifyConnectionIssues(string connectionIdentifier);
    }
}
