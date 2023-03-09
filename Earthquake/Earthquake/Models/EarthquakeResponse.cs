using Newtonsoft.Json;

namespace Earthquake.API.Models
{
    public class EarthquakeResponse
    {
        public Guid Id { get; set; }
        public decimal Magnitude { get; set; }
        public string? Place { get; set; }
        public string? Type { get; set; }
        public List<double>? Coordinates { get; set; }

        public EarthquakeResponse() { }

        public EarthquakeResponse(Guid id, decimal magnitude, string place, string type, List<double> coordinates) 
        {
            Id = id;
            Magnitude = magnitude;
            Place = place;
            Type = type;
            Coordinates = coordinates;
        }
    }
}
