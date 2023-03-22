using AutoMapper;
using Earthquake;
using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.API.Services;
using Earthquake.Controllers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EartquakesAPI.Tests.ProcessorTests
{
    public class EarthquakeProcessorTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IEarthquakeRepository> _earthquakeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public EarthquakeProcessorTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _earthquakeRepositoryMock = new Mock<IEarthquakeRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async void GetLatestEarthquakeFromRomania_HttpBadRequest()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    //Content = new StringContent(""),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            /// Act
            var result = await processor.GetLatestEarthquakeFromRomania() as BadRequestObjectResult;

            /// Assert
            //Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void GetLatestEarthquakeFromRomania_BadRequestResult()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"type\":\"FeatureCollection\",\"metadata\":{\"generated\":1679481416000,\"url\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&latitude=45.9432&longitude=24.9668&maxradiuskm=500&limit=1\",\"title\":\"USGSEarthquakes\",\"status\":200,\"api\":\"1.13.6\",\"limit\":1,\"offset\":1,\"count\":1},\"features\":[{\"type\":\"Feature\",\"properties\":{\"mag\":4.9,\"place\":\"3kmWSWofArcani,Romania\",\"time\":1679320935362,\"updated\":1679476184270,\"tz\":null,\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\",\"felt\":30,\"cdi\":8.1,\"mmi\":null,\"alert\":null,\"status\":\"reviewed\",\"tsunami\":0,\"sig\":394,\"net\":\"us\",\"code\":\"7000jlf8\",\"ids\":\",us7000jlf8,\",\"sources\":\",us,\",\"types\":\",dyfi,origin,phase-data,\",\"nst\":91,\"dmin\":1.155,\"rms\":0.9,\"gap\":30,\"magType\":\"mb\",\"type\":\"earthquake\",\"title\":\"M4.9-3kmWSWofArcani,Romania\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[23.0866,45.0762,10]},\"id\":\"us7000jlf8\"}]}"),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            EarthquakeEntity earthquakeEntity = new EarthquakeEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Properties = new Properties()
                {
                    Magnitude = (decimal)4.9,
                    Place = "3 km WSW of Arcani, Romania",
                    TimeMs = 1679320935362,
                    Updated = 1679483643771,
                    Url = "\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\"",
                    Tz = null,
                },
                Geometry = new Geometry()
                {
                    Type = "Point",
                    Coordinates = new List<double>()
                    {
                        23.0866,
                        45.0762,
                        10
                    }
                }
            };

            _mapperMock.Setup(x => x.Map<EarthquakeEntity>(It.IsAny<Feature>())).Returns(earthquakeEntity);

            _earthquakeRepositoryMock.Setup(x => x.Create(It.IsAny<EarthquakeEntity>())).ReturnsAsync(false);

            /// Act
            var result = await processor.GetLatestEarthquakeFromRomania() as BadRequestResult;

            /// Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void GetLatestEarthquakeFromRomania_OK()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"type\":\"FeatureCollection\",\"metadata\":{\"generated\":1679481416000,\"url\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&latitude=45.9432&longitude=24.9668&maxradiuskm=500&limit=1\",\"title\":\"USGSEarthquakes\",\"status\":200,\"api\":\"1.13.6\",\"limit\":1,\"offset\":1,\"count\":1},\"features\":[{\"type\":\"Feature\",\"properties\":{\"mag\":4.9,\"place\":\"3kmWSWofArcani,Romania\",\"time\":1679320935362,\"updated\":1679476184270,\"tz\":null,\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\",\"felt\":30,\"cdi\":8.1,\"mmi\":null,\"alert\":null,\"status\":\"reviewed\",\"tsunami\":0,\"sig\":394,\"net\":\"us\",\"code\":\"7000jlf8\",\"ids\":\",us7000jlf8,\",\"sources\":\",us,\",\"types\":\",dyfi,origin,phase-data,\",\"nst\":91,\"dmin\":1.155,\"rms\":0.9,\"gap\":30,\"magType\":\"mb\",\"type\":\"earthquake\",\"title\":\"M4.9-3kmWSWofArcani,Romania\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[23.0866,45.0762,10]},\"id\":\"us7000jlf8\"}]}"),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            EarthquakeEntity earthquakeEntity = new EarthquakeEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Properties = new Properties()
                {
                    Magnitude = (decimal)4.9,
                    Place = "3 km WSW of Arcani, Romania",
                    TimeMs = 1679320935362,
                    Updated = 1679483643771,
                    Tz = null,
                },
                Geometry = new Geometry()
                {
                    Type = "Point",
                    Coordinates = new List<double>()
                    {
                        23.0866,
                        45.0762,
                        10
                    }
                }
            };

            _mapperMock.Setup(x => x.Map<EarthquakeEntity>(It.IsAny<Feature>())).Returns(earthquakeEntity);

            _earthquakeRepositoryMock.Setup(x => x.Create(It.IsAny<EarthquakeEntity>())).ReturnsAsync(true);

            EarthquakeResponse earthquakeResponse = new EarthquakeResponse()
            {
                Id = Guid.NewGuid(),
                Magnitude = (decimal)4.9,
                Place = "3 km WSW of Arcani, Romania",
                Type = "earthquake",
                Coordinates = new List<double>()
                    {
                        23.0866,
                        45.0762,
                        10
                    }
            };

            _mapperMock.Setup(x => x.Map<EarthquakeResponse>(It.IsAny<EarthquakeEntity>())).Returns(earthquakeResponse);

            /// Act
            var result = await processor.GetLatestEarthquakeFromRomania() as OkObjectResult;

            /// Assert
            //Assert.Equal(earthquakeResponse, result?.Value as EarthquakeResponse);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void GetEarthquakesWithParams_BadRequest()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    //Content = new StringContent(""),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            EarthquakeRequest earthquakeRequest = new()
            {
                StartTime = new DateTime(2019, 01, 01),
                EndTime = new DateTime(2019, 01, 02),
                MaxMagnitude = 5,
                OrderBy = "time"
            };

            /// Act
            var result = await processor.GetEarthquakesByParams(earthquakeRequest) as BadRequestObjectResult;

            /// Assert
            //Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void GetEarthquakesWithParams_BadRequestResult()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"type\":\"FeatureCollection\",\"metadata\":{\"generated\":1679481416000,\"url\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&latitude=45.9432&longitude=24.9668&maxradiuskm=500&limit=1\",\"title\":\"USGSEarthquakes\",\"status\":200,\"api\":\"1.13.6\",\"limit\":1,\"offset\":1,\"count\":1},\"features\":[{\"type\":\"Feature\",\"properties\":{\"mag\":4.9,\"place\":\"3kmWSWofArcani,Romania\",\"time\":1679320935362,\"updated\":1679476184270,\"tz\":null,\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\",\"felt\":30,\"cdi\":8.1,\"mmi\":null,\"alert\":null,\"status\":\"reviewed\",\"tsunami\":0,\"sig\":394,\"net\":\"us\",\"code\":\"7000jlf8\",\"ids\":\",us7000jlf8,\",\"sources\":\",us,\",\"types\":\",dyfi,origin,phase-data,\",\"nst\":91,\"dmin\":1.155,\"rms\":0.9,\"gap\":30,\"magType\":\"mb\",\"type\":\"earthquake\",\"title\":\"M4.9-3kmWSWofArcani,Romania\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[23.0866,45.0762,10]},\"id\":\"us7000jlf8\"}]}"),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            EarthquakeEntity earthquakeEntity = new EarthquakeEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Properties = new Properties()
                {
                    Magnitude = (decimal)4.9,
                    Place = "3 km WSW of Arcani, Romania",
                    TimeMs = 1679320935362,
                    Updated = 1679483643771,
                    Url = "\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\"",
                    Tz = null,
                },
                Geometry = new Geometry()
                {
                    Type = "Point",
                    Coordinates = new List<double>()
                    {
                        23.0866,
                        45.0762,
                        10
                    }
                }
            };

            _mapperMock.Setup(x => x.Map<EarthquakeEntity>(It.IsAny<Feature>())).Returns(earthquakeEntity);

            EarthquakeRequest earthquakeRequest = new()
            {
                StartTime = new DateTime(2019, 01, 01),
                EndTime = new DateTime(2019, 01, 02),
                MaxMagnitude = 5,
                OrderBy = "time"
            };

            _earthquakeRepositoryMock.Setup(x => x.CreateMany(It.IsAny<List<EarthquakeEntity>>())).ReturnsAsync(false);

            /// Act
            var result = await processor.GetEarthquakesByParams(earthquakeRequest) as BadRequestResult;

            /// Assert
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async void GetEarthquakesWithParams_OK()
        {
            /// Arange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"type\":\"FeatureCollection\",\"metadata\":{\"generated\":1679481416000,\"url\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&latitude=45.9432&longitude=24.9668&maxradiuskm=500&limit=1\",\"title\":\"USGSEarthquakes\",\"status\":200,\"api\":\"1.13.6\",\"limit\":1,\"offset\":1,\"count\":1},\"features\":[{\"type\":\"Feature\",\"properties\":{\"mag\":4.9,\"place\":\"3kmWSWofArcani,Romania\",\"time\":1679320935362,\"updated\":1679476184270,\"tz\":null,\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\",\"felt\":30,\"cdi\":8.1,\"mmi\":null,\"alert\":null,\"status\":\"reviewed\",\"tsunami\":0,\"sig\":394,\"net\":\"us\",\"code\":\"7000jlf8\",\"ids\":\",us7000jlf8,\",\"sources\":\",us,\",\"types\":\",dyfi,origin,phase-data,\",\"nst\":91,\"dmin\":1.155,\"rms\":0.9,\"gap\":30,\"magType\":\"mb\",\"type\":\"earthquake\",\"title\":\"M4.9-3kmWSWofArcani,Romania\"},\"geometry\":{\"type\":\"Point\",\"coordinates\":[23.0866,45.0762,10]},\"id\":\"us7000jlf8\"}]}"),
                });

            var client = new HttpClient(httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            EarthquakeProcessor processor = new(_httpClientFactoryMock.Object, _earthquakeRepositoryMock.Object, _mapperMock.Object);

            EarthquakeEntity earthquakeEntity = new EarthquakeEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Properties = new Properties()
                {
                    Magnitude = (decimal)4.9,
                    Place = "3 km WSW of Arcani, Romania",
                    TimeMs = 1679320935362,
                    Updated = 1679483643771,
                    Url = "\"url\":\"https://earthquake.usgs.gov/earthquakes/eventpage/us7000jlf8\",\"detail\":\"https://earthquake.usgs.gov/fdsnws/event/1/query?eventid=us7000jlf8&format=geojson\"",
                    Tz = null,
                },
                Geometry = new Geometry()
                {
                    Type = "Point",
                    Coordinates = new List<double>()
                    {
                        23.0866,
                        45.0762,
                        10
                    }
                }
            };

            _mapperMock.Setup(x => x.Map<EarthquakeEntity>(It.IsAny<Feature>())).Returns(earthquakeEntity);

            EarthquakeRequest earthquakeRequest = new()
            {
                StartTime = new DateTime(2019, 01, 01),
                EndTime = new DateTime(2019, 01, 02),
                MaxMagnitude = 5,
                OrderBy = "time"
            };

            _earthquakeRepositoryMock.Setup(x => x.CreateMany(It.IsAny<List<EarthquakeEntity>>())).ReturnsAsync(true);

            EarthquakeResponse earthquakeResponse = new() { };

            _mapperMock.Setup(x => x.Map<EarthquakeResponse>(It.IsAny<EarthquakeEntity>())).Returns(earthquakeResponse);

            /// Act
            var result = await processor.GetEarthquakesByParams(earthquakeRequest) as OkObjectResult;

            /// Assert
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }
    }
}
