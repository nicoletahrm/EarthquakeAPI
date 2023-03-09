using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Processor
{
    public interface IEarthquakeProcessor
    {
        Task<IActionResult> GetLatestEarthquakeFromRomania();
    }
}
