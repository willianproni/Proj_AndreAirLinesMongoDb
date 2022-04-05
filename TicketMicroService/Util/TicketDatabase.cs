namespace TicketMicroService.Util
{
    public class TicketDatabase : ITicketDatabase
    {
        public string TicketCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
