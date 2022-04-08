namespace AccessMicroService.Util
{
    public class AccessDatabase : IAccessDatabase
    {
        public string AccessCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
