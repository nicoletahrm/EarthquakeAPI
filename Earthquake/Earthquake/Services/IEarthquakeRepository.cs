namespace Earthquake.API.Services
{
    public interface IEarthquakeRepository
    {
        Task<bool> Create(Earthquake earthquake);
    }
}
