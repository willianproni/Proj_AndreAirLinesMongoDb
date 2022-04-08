namespace UserMicroService.Util
{
    public interface IUserDatabase
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
