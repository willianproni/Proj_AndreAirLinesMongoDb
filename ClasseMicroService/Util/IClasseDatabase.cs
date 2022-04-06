namespace ClasseMicroService
{
    public interface IClasseDatabase
    {
        string ClasseCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
