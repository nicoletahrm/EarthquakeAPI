using Earthquake.API.Models;
using Earthquake.API.Models.Requests;
using Earthquake.API.Processor;
using Earthquake.API.Validations;
using FluentValidation.TestHelper;

namespace EartquakesAPI.Tests.ValidationsTests
{
    public class EarthquakeRequestValidatorTests
    {
        private readonly EarthquakeRequestValidator _earthquakeRequestValidator;

        public EarthquakeRequestValidatorTests()
        {
            _earthquakeRequestValidator = new EarthquakeRequestValidator();
        }

        [Fact]
        public void ShouldNotHaveErrors()
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Parse("2019,01,01"), DateTime.Parse("2019,01,02"), 5, "time"));

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => new 
            {
                x.StartTime, 
                x.EndTime,
                x.MaxMagnitude,
                x.OrderBy
            });
        }

        [Theory]
        [InlineData("2019, 01, 02")]
        [InlineData("2019, 02, 01")]
        [InlineData("2020, 01, 01")]
        public void ShouldHaveErrorWhenStartTimeIsAfterEndTime(string startTime)
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Parse(startTime), DateTime.Parse("2019, 01, 01"), 5, "time"));

            //Assert
            result.ShouldHaveValidationErrorFor(x => new 
            {
                x.StartTime, 
                x.EndTime 
            });
        }

        [Fact]
        public void ShouldHaveErrorWhenStartTimeIsAfterCurrentDate()
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Now.AddDays(1), DateTime.Now, 5, "time"));

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.StartTime)
            .WithErrorMessage("StartTime is not a valid date.");
        }

        [Fact]
        public void ShouldHaveErrorWhenEndTimeIsAfterCurrentDate()
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Now, DateTime.Now.AddDays(1), 5, "time"));

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.EndTime)
            .WithErrorMessage("EndTime is not a valid date.");
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-2)]
        public void ShouldHaveErrorWhenMaxMagnitudeIsNotInSpecificRange(int maxmagnitude)
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Parse("2019,01,01"), DateTime.Parse("2019,01,02"), maxmagnitude, "time"));

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.MaxMagnitude);
        }

        [Theory]
        [InlineData("timeasc")]
        [InlineData("magnitudeasc")]
        public void ShouldHaveErrorWhenOrderByIsNotCorrect(string orderBy)
        {
            //Arrange

            //Act
            var result = _earthquakeRequestValidator.TestValidate(CreateEarthquakeRequest(DateTime.Parse("2019,01,01"), DateTime.Parse("2019,01,02"), 5, orderBy));

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.OrderBy)
                .WithErrorMessage("OrderBy is not valid. Try time, time-asc, magnitute or magnitude-asc.");
        }

        private static EarthquakeRequest CreateEarthquakeRequest(DateTime startTime, DateTime endTime, decimal maxmagnitude, string orderBy) 
        {
            return new EarthquakeRequest
            {
                StartTime = startTime,
                EndTime = endTime,
                MaxMagnitude = maxmagnitude,
                OrderBy = orderBy
            };
        }
    }
}
