using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Microservice1.Data;
using Weather.Microservice1.Entities;

namespace Weather.Microservice1.Repositories
{
    public class ForecastDataRepository : IForecastDataRepository
    {
        private readonly IForecastDataContext _context;

        public ForecastDataRepository(IForecastDataContext context)
        {
            _context = context;
        }

        public async Task CreateForecast(ForecastData forecast)
        {
            await _context.ForecastsData.InsertOneAsync(forecast);
        }

        public async Task<ForecastData> GetForecast(string id)
        {
            return await _context.ForecastsData.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ForecastData>> GetForecasts()
        {
            return await _context.ForecastsData.Find(p => true).ToListAsync();
        }
    }
}
