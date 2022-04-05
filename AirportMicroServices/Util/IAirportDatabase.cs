namespace AirportMicroServices.Util
{
    public interface IAirportDatabase
    {
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
