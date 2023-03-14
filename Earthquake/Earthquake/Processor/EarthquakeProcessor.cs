﻿using AutoMapper;
using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Extensions;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

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
            var earthquake = JsonConvert.DeserializeObject<Earthquake>(content).Features.FirstOrDefault();

            EarthquakeEntity earthquakeEntity = _mapper.Map<EarthquakeEntity>(earthquake);
            earthquakeEntity.Id = Guid.NewGuid().ToString();

            if (await _earthquakeRepository.Create(earthquakeEntity))
            {
                EarthquakeResponse earthquakeResponse = _mapper.Map<EarthquakeResponse>(earthquakeEntity);

                return new ObjectResult(earthquakeResponse);
             }

            return new ObjectResult(null);
        }

        public async Task<IActionResult> GetEarthquakesByParams(EarthquakeRequest earthquakeRequest)
        {
            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = new();

            //if (earthquakeRequest.MaxMagnitude != null)
            //{
                string maxmagnitude = earthquakeRequest.MaxMagnitude.ToString();
                string orderBy = earthquakeRequest.OrderBy.ToString();

                response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}&maxmagnitude={maxmagnitude}&orderby={orderBy}");
            //}
            //else
            //{
               // response = await httpClient.GetAsync($"{_baseUrl}&starttime={earthquakeRequest.StartTime}&endtime={earthquakeRequest.EndTime}&maxmagnitude={maxmagnitude}");
           // }

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
                return new ObjectResult(earthquakeResponses);
            }

            return new ObjectResult(null);
        }
    }
}
