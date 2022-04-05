namespace TicketMicroService.Util
{
    public interface ITicketDatabase
    {
        string TicketCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
