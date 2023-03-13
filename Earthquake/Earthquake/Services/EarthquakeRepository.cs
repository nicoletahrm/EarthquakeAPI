using Earthquake.API.Models;
using Earthquake.API.Settings;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Text.Json.Serialization;

namespace Earthquake.API.Services
{
    public class EarthquakeRepository : IEarthquakeRepository
    {
        private readonly IMongoCollection<EarthquakeEntity> _earthquakes;

        public EarthquakeRepository(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _earthquakes = database.GetCollection<EarthquakeEntity>(settings.EarthquakesCollectionName);
        }

        public async Task<bool> Create(EarthquakeEntity earthquake)
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

        public async Task<bool> CreateMany(List<EarthquakeEntity> earthquakeEntities)
        {
            try
            {
                await _earthquakes.InsertManyAsync(earthquakeEntities);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
