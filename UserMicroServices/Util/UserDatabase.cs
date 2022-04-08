namespace UserMicroService.Util
{
    public class UserDatabase : IUserDatabase
    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
