using Earthquake;
using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EartquakesAPI.Tests.ControllerTests
{
    public class ErthquakeControllerTests
    {
        private readonly Mock<IEarthquakeProcessor> _earthquakeProcessorMock;

        public ErthquakeControllerTests()
        {
            _earthquakeProcessorMock = new Mock<IEarthquakeProcessor>();
        }

        [Fact]
        public async void ReturnLatestEarthquakeFromRomania_Success()
        {
            //Arrange
            Guid earthquakeId = Guid.NewGuid();

            EarthquakeResponse earthquakeResponse = new()
            {
                Id = earthquakeId,
                Magnitude = (decimal)4.2,
                Place = "Romania",
                Type = "earthquake",
                Coordinates = new List<double>
                {
                    26.3132,
                    45.4973,
                    137.246
                }
            };

            _earthquakeProcessorMock.Setup(e => e.GetLatestEarthquakeFromRomania()).ReturnsAsync(new OkObjectResult(earthquakeResponse));

            EarthquakeController controller = new(_earthquakeProcessorMock.Object);

            //Act
            IActionResult result = await controller.GetLatestEarthquakeFromRomania();

            //Assert
            Assert.Equal(earthquakeResponse, (result as OkObjectResult)?.Value as EarthquakeResponse);
            _earthquakeProcessorMock.Verify(e => e.GetLatestEarthquakeFromRomania(), Times.Once);
        }

        [Fact]
        public async void ReturnEarthquakesByParams_Success()
        {
            //Arrange
            EarthquakeRequest earthquakeRequest = new()
            {
                StartTime = new DateTime(2019, 01, 01),
                EndTime = new DateTime(2019, 01, 02),
                MaxMagnitude = 5,
                OrderBy = "time"
            };

            List<EarthquakeResponse> earthquakeResponses = new()
            {
                new EarthquakeResponse {},
                new EarthquakeResponse {},
                new EarthquakeResponse {}
            };

            _earthquakeProcessorMock.Setup(e => e.GetEarthquakesByParams(It.IsAny<EarthquakeRequest>())).ReturnsAsync(new OkObjectResult(earthquakeResponses));

            EarthquakeController controller = new(_earthquakeProcessorMock.Object);

            //Act
            IActionResult result = await controller.GetEarthquakesByParams(earthquakeRequest);

            //Assert
            Assert.Equal(earthquakeResponses, (result as OkObjectResult)?.Value as List<EarthquakeResponse>);
            _earthquakeProcessorMock.Verify(e => e.GetEarthquakesByParams(It.IsAny<EarthquakeRequest>()), Times.Once);
        }
    }
}