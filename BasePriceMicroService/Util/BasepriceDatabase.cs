namespace BasePriceMicroService.Util
{
    public class BasepriceDatabase : IBasepriceDatabase
    {
       public string BasepriceCollectionName { get; set; }
       public string ConnectionString { get; set; }
       public string DatabaseName { get; set; }
    }
}
