namespace FlightMicroService.Util
{
    public interface IFlightDatabase
    {
        string FlightCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
