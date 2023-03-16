using Earthquake;
using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EartquakesAPI.Tests
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
            //Guid earthquakeId = Guid.NewGuid();

            //EarthquakeResponse earthquakeResponse = new()
            //{
            //    Id = earthquakeId,
            //    Magnitude = (decimal)4.2,
            //    Place = "Romania",
            //    Type = "earthquake",
            //    Coordinates = new List<double> 
            //    {
            //        26.3132,
            //        45.4973,
            //        137.246
            //    }
            //};

            //_earthquakeProcessorMock.Setup(e => e.GetLatestEarthquakeFromRomania()).Returns(Task.FromResult(earthquakeId));

            //EarthquakeController controller = new(_earthquakeProcessorMock.Object);
            //IActionResult result = await controller.GetLatestEarthquakeFromRomania();

            //Assert
            //Assert.Equal(earthquakeResponse, (result as OkObjectResult)?.Value as EarthquakeResponse);
            //_earthquakeProcessorMock.Verify(e => e.GetLatestEarthquakeFromRomania(), Times.Once);
        }
    }
}