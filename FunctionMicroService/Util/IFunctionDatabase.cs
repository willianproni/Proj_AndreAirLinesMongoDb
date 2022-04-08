namespace FunctionMicroService.Util
{
    public interface IFunctionDatabase
    {
        string FunctionCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
