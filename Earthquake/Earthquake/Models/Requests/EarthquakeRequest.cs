using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Earthquake.API.Models.Requests
{
    public class EarthquakeRequest
    {
        [Required]
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? MaxMagnitude { get; set; } 
        public string? OrderBy { get; set; }
    }
}
