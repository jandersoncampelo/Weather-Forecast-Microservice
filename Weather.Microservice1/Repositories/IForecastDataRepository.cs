using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Microservice1.Entities;

namespace Weather.Microservice1.Repositories
{
    public interface IForecastDataRepository
    {
        Task CreateForecast(ForecastData forecast);
        Task<IEnumerable<ForecastData>> GetForecasts();
        Task<ForecastData> GetForecast(string id);
    }
}
