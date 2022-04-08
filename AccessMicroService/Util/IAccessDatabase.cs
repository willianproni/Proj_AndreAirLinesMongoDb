namespace AccessMicroService.Util
{
    public interface IAccessDatabase
    {
        string AccessCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
