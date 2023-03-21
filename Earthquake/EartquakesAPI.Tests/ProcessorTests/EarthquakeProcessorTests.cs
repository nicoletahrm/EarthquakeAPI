using AutoMapper;
using Earthquake.API.Models;
using Earthquake.API.Processor;
using Earthquake.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EartquakesAPI.Tests.ProcessorTests
{
    public class EarthquakeProcessorTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IEarthquakeRepository> _earthquakeRepostoryMock;
        private readonly Mock<IMapper> _mapperMock;


        public EarthquakeProcessorTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _earthquakeRepostoryMock = new Mock<IEarthquakeRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void ReturnBadRequestIfEarthquakeIsInvalid()
        {
            //Arange
            _httpClientFactoryMock.Setup(x => x.CreateClient()).Returns(new HttpClient());

            var result = _httpClientFactoryMock.Object.CreateClient();

            //Act

            //Assert
           
        }
    }
}
