namespace AircraftMicroService.Util
{
    public interface IAircraftDatabase
    {
        string AircraftCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
