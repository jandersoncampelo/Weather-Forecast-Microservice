using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Weather.Microservice1.Entities;

namespace Weather.Microservice1.Data
{
    public class ForecastDataContext : IForecastDataContext
    {
        public ForecastDataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            ForecastsData = database.GetCollection<ForecastData>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<ForecastData> ForecastsData { get; }
    }
}
