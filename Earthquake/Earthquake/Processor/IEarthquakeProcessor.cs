using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Processor
{
    public interface IEarthquakeProcessor
    {
        Task<IActionResult> GetLatestEarthquakeFromRomania();

        Task<IActionResult> GetEarthquakesByParams(String startTime, String endTime, Decimal maxmagnitude, String orderBy);
    }
}
