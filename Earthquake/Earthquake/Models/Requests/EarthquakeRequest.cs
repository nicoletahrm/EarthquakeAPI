namespace Earthquake.API.Models.Requests
{
    public class EarthquakeRequest
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Decimal MaxMagnitude { get; set; } 
        public String? OrderBy { get; set; }
    }
}
