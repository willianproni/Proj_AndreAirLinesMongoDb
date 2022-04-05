namespace PassengerMicroService.Util
{
    public interface IPassengerDatabase
    {
        string PassengerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
