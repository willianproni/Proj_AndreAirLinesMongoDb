namespace AircraftMicroService.Util
{
    public class AircraftDatabase : IAircraftDatabase
    {
        public string AircraftCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
