namespace Earthquake.API.Settings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string EarthquakesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
