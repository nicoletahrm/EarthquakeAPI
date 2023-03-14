namespace Earthquake.API.Models.Requests
{
    public class EarthquakeRequest
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Decimal? Maxmagnitude { get; set; } 
        public String? OrderBy { get; set; }
    }
}
