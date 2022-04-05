namespace FlightMicroService.Util
{
    public class FlightDatabase : IFlightDatabase
    {
        public string FlightCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
