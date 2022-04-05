namespace AirportMicroService.Util
{
    public class AirportDatabase : IAirportDatabase
    {
        public string AirportCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
