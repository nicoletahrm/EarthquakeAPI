using Earthquake.API.Settings;
using MongoDB.Driver;

namespace Earthquake.API.Services
{
    public class EarthquakeRepository : IEarthquakeRepository
    {
        private readonly IMongoCollection<Earthquake> _earthquakes;

        public EarthquakeRepository(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _earthquakes = database.GetCollection<Earthquake>(settings.EarthquakesCollectionName);
        }

        public async Task<bool> Create(Earthquake earthquake)
        {
            try
            {
                await _earthquakes.InsertOneAsync(earthquake);

                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
