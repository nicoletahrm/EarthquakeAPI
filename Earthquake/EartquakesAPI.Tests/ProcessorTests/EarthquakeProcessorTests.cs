using Earthquake.API.Processor;
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
        private readonly Mock<IEarthquakeProcessor> _earthquakeProcessor;

        public EarthquakeProcessorTests()
        {
            _earthquakeProcessor = new Mock<IEarthquakeProcessor>();
        }

        [Fact]
        public void ReturnBadRequestIfEarthquakeIsInvalid()
        {

        }
    }
}
