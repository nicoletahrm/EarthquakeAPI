using AutoMapper;
using Earthquake.API.Models;
using Earthquake.API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Earthquake.API.Processor
{
    public class EarthquakeProcessor : IEarthquakeProcessor
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _baseUrl = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson";
        private readonly IEarthquakeRepository _earthquakeRepository;
        private readonly IMapper _mapper;

        public EarthquakeProcessor(IHttpClientFactory httpClientFactory, IEarthquakeRepository earthquakeRepository, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _earthquakeRepository = earthquakeRepository ?? throw new ArgumentNullException(nameof(earthquakeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> GetLatestEarthquakeFromRomania()
        {
            var httpClient = _httpClientFactory.CreateClient();

            string lat = "45.9432";
            string lon = "24.9668";
            string radius = "500";

            var response = await httpClient.GetAsync($"{_baseUrl}&latitude=45.9432&longitude=24.9668&maxradiuskm=500&limit=1&latitude={lat}&longitude={lon}&maxradiuskm={radius}&limit=");

            if (!response.IsSuccessStatusCode)
            {
                return new BadRequestObjectResult("Error earthquake invalid data.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var earthquake = JsonConvert.DeserializeObject<Earthquake>(content);
            earthquake.Id = Guid.NewGuid().ToString();

            
            if (await _earthquakeRepository.Create(earthquake))
            {
                EarthquakeResponse earthquakeResponse = _mapper.Map<EarthquakeResponse>(earthquake);

                return new ObjectResult(earthquakeResponse);
             }

            return new ObjectResult(null);

        }
    }
}
