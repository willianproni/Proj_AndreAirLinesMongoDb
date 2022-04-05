namespace BasePriceMicroService.Util
{
    public interface IBasepriceDatabase
    {
        string BasepriceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
