using RestSharp;
using System.Threading.Tasks;

namespace Wheater.Microservice2.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public async Task<string> GetForecast(string latitude, string longitute)
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/");
            var request = new RestRequest("forecast");

            client.AddDefaultParameter("lat", latitude, ParameterType.GetOrPost);
            client.AddDefaultParameter("lon", longitute, ParameterType.GetOrPost);
            client.AddDefaultParameter("appid", "06fe46f7ab5bb6942c03ad721cced379", ParameterType.GetOrPost);

            var response = await client.GetAsync(request);

            return response.Content;
        }
    }
}
