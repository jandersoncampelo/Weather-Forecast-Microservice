using System.Threading.Tasks;

namespace Wheater.Microservice2.Services
{
    public interface IWeatherForecastService
    {
        Task<string> GetForecastData(string cityName);
        Task SendForecastData(string cityName);
    }
}
