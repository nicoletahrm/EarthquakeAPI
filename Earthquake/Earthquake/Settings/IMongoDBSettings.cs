namespace Earthquake.API.Settings
{
    public interface IMongoDBSettings
    {
        string EarthquakesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
