using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.Controllers
{
    [ApiController]
    [Route("api/earthquakes")]
    public class EarthquakeController : ControllerBase
    {
        private readonly IEarthquakeProcessor _earthquakeProcessor;

        public EarthquakeController(IEarthquakeProcessor earthquakeProcessor)
        {
            _earthquakeProcessor = earthquakeProcessor ?? throw new ArgumentNullException(nameof(earthquakeProcessor));
        }

        [HttpGet("latest-earthquake-romania")]
        public async Task<IActionResult> GetLatestEarthquakeFromRomania()
        {
            return await _earthquakeProcessor.GetLatestEarthquakeFromRomania();

        }

        [HttpGet("earthquakes-by-params")]
        public async Task<IActionResult> GetEarthquakesByParams([FromQuery] EarthquakeRequest earthquakeRequest)
        {
            EarthquakeRequest earthquake = new()
            {
                StartTime = earthquakeRequest.StartTime,
                EndTime = earthquakeRequest.EndTime,
                MaxMagnitude = earthquakeRequest.MaxMagnitude,
                OrderBy = earthquakeRequest.OrderBy
            };

            return await _earthquakeProcessor.GetEarthquakesByParams(earthquake);
        }
    }
}
