namespace FunctionMicroService.Util
{
    public class FunctionDatabase : IFunctionDatabase
    {
        public string FunctionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
