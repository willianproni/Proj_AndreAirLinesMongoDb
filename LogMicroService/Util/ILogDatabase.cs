namespace LogMicroService.Util
{
    public interface ILogDatabase
    {
        string LogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
