using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Xunit;
using Shouldly;
using Earthquake.API.Models;

namespace EartquakesAPI.Tests.ProfilesTests
{
    public class EarthquakeProfileTests
    {
        [Fact]
        public void Test_CreateMap_Earthquake_To_EarthquakeResponse()
        {
            var target = GetTarget();

            var earthquake = new Earthquake(target);

            var result = target.Map<EarthquakeResponse>(earthquake);

        }
        private IMapper GetTarget()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<Earthquake.API.Profiles>());
            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
