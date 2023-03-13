using Earthquake.API.Models;

namespace Earthquake.API.Services
{
    public interface IEarthquakeRepository
    {
        Task<bool> Create(EarthquakeEntity earthquake);
        Task<bool> CreateMany(List<EarthquakeEntity> features);
    }
}
