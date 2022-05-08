using System.Threading.Tasks;

namespace Wheater.Microservice2.Services
{
    public interface IWeatherForecastService
    {
        Task<string> GetForecast(string latitude, string longitute);
    }
}
