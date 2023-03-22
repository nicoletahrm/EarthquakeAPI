using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Xunit;
using Shouldly;
using Earthquake.API.Models;
using Earthquake;
using Earthquake.API.Profiles;

namespace EartquakesAPI.Tests.ProfilesTests
{
    public class EarthquakeProfileTests
    {
        [Fact]
        public void Test_CreateMap_Feature_To_EarthquakeResponse()
        {
            var target = GetTarget();

            Feature earthquakeFeature = new Feature()
            {
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

            var result = target.Map<EarthquakeEntity>(earthquakeFeature);

            result.Properties.Time.ShouldBe(earthquakeFeature.Properties.Time);
            result.Properties.Type.ShouldBe(earthquakeFeature.Properties.Type);
            result.Geometry.Coordinates.ShouldBe(earthquakeFeature.Geometry.Coordinates);
        }

        [Fact]
        public void Test_CreateMap_EarthquakeEntity_To_EarthquakeResponse()
        {
            var target = GetTarget();

            EarthquakeEntity earthquakEntity = new EarthquakeEntity()
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

            var result = target.Map<EarthquakeEntity>(earthquakEntity);

            result.Id.ShouldBe(earthquakEntity.Id);
            result.Properties.Magnitude.ShouldBe(earthquakEntity.Properties.Magnitude);
            result.Properties.Type.ShouldBe(earthquakEntity.Properties.Type);
            result.Geometry.Coordinates.ShouldBe(earthquakEntity.Geometry.Coordinates);
        }

        private IMapper GetTarget()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EarthquakeProfile>());
            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
