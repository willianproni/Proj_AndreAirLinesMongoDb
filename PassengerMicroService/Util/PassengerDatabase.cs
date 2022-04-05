namespace PassengerMicroService.Util
{
    public class PassengerDatabase : IPassengerDatabase
    {
        public string PassengerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
