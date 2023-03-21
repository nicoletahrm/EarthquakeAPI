using AutoMapper;
using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
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

            var response = await httpClient.GetAsync($"{_baseUrl}&latitude={lat}&longitude={lon}&maxradiuskm={radius}&limit=1");

            if (!response.IsSuccessStatusCode)
            {
                return new BadRequestObjectResult("Error earthquake invalid data.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var earthquake = JsonConvert.DeserializeObject<Earthquake>(content).Features.FirstOrDefault();

            EarthquakeEntity earthquakeEntity = _mapper.Map<EarthquakeEntity>(earthquake);
            earthquakeEntity.Id = Guid.NewGuid().ToString();

            if (await _earthquakeRepository.Create(earthquakeEntity))
            {
                EarthquakeResponse earthquakeResponse = _mapper.Map<EarthquakeResponse>(earthquakeEntity);

                return new OkObjectResult(earthquakeResponse);
             }

            return new BadRequestResult();
        }

        public async Task<IActionResult> GetEarthquakesByParams(EarthquakeRequest earthquakeRequest)
        {
            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = new();

            string maxmagnitude = earthquakeRequest.MaxMagnitude?.ToString();
            string orderBy = earthquakeRequest.OrderBy?.ToString();

            switch (maxmagnitude, orderBy)
            {
                case ("null", "null"):
                    response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}");
                    break;
                case ("null", _):
                    response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}&orderby={orderBy}");
                    break;
                case (_, "null"):
                    response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}&maxmagnitude={maxmagnitude}");
                    break;
                default:
                    response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}&maxmagnitude={maxmagnitude}&orderby={orderBy}");
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                return new BadRequestObjectResult("Error earthquake invalid data.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var earthquakesFeatures = JsonConvert.DeserializeObject<Earthquake>(content).Features;

            List<EarthquakeEntity> responseList = new();

            foreach (var earthquakeFeature in earthquakesFeatures)
            {
                EarthquakeEntity earthquakeEntity = _mapper.Map<EarthquakeEntity>(earthquakeFeature);
                earthquakeEntity.Id = Guid.NewGuid().ToString();
                responseList.Add(earthquakeEntity);
            }

            List<EarthquakeResponse> earthquakeResponses = new();

            if (await _earthquakeRepository.CreateMany(responseList))
            {
                foreach (var earthquake in responseList)
                {
                    EarthquakeResponse earthquakeResponse = _mapper.Map<EarthquakeResponse>(earthquake);
                    earthquakeResponses.Add(earthquakeResponse);
                }
                return new OkObjectResult(earthquakeResponses);
            }

            return new BadRequestResult();
        }
    }
}
