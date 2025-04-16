namespace BachataApi.Configuration
{
    public class MongoDbSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required string FigurasCollectionName { get; set; }
    }
}
