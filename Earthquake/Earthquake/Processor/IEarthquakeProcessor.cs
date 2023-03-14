using Earthquake.API.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Processor
{
    public interface IEarthquakeProcessor
    {
        Task<IActionResult> GetLatestEarthquakeFromRomania();

        Task<IActionResult> GetEarthquakesByParams(EarthquakeRequest earthquakeRequest);
    }
}
