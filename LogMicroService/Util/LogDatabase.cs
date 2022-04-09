namespace LogMicroService.Util
{
    public class LogDatabase : ILogDatabase
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
