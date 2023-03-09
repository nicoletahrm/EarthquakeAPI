﻿using Earthquake.API.Models;
using Earthquake.API.Processor;
using Earthquake.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.Controllers
{
    [ApiController]
    [Route("api/earthquakes")]
    public class EarthquakeController : ControllerBase
    {
        private readonly IEarthquakeProcessor _earthquakeProcessor;

        public EarthquakeController(IEarthquakeProcessor earthquakeProcessor, IEarthquakeRepository earthquakeRepository)
        {
            _earthquakeProcessor = earthquakeProcessor ?? throw new ArgumentNullException(nameof(earthquakeProcessor));
        }

        [HttpGet("latest-earthquake-romania")]
        public async Task<IActionResult> GetLatestEarthquakeFromRomania()
        {
            return await _earthquakeProcessor.GetLatestEarthquakeFromRomania();

        }
    }
}
