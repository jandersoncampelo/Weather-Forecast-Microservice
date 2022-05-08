using MongoDB.Driver;
using Weather.Microservice1.Entities;

namespace Weather.Microservice1.Data
{
    public interface IForecastDataContext
    {
        IMongoCollection<ForecastData> ForecastsData { get; }
    }
}
