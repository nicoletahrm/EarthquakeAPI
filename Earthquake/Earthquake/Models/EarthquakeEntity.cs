using MongoDB.Bson.Serialization.Attributes;

namespace Earthquake.API.Models
{
    public class EarthquakeEntity
    {
        [BsonId]
        public string? Id { get; set; }
        public Properties? Properties { get; set; }
        public Geometry? Geometry { get; set; }
    }
}
