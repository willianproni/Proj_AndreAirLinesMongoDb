namespace ClasseMicroService.Util
{
    public class ClasseDatabase : IClasseDatabase
    {
        public string ClasseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
