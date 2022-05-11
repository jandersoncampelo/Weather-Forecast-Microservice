using RabbitMQ.Client;
using RestSharp;
using System.Text;
using System.Threading.Tasks;

namespace Wheater.Microservice2.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private const string QUEUE_NAME = "ForecastData";
        private readonly RabbitMQConfigurations _rabbitConfigurations;

        public WeatherForecastService(RabbitMQConfigurations rabbitConfigurations)
        {
            _rabbitConfigurations = rabbitConfigurations;
        }

        public async Task<string> GetForecastData(string cityName)
        {
            var client = new RestClient("https://api.openweathermap.org/data/2.5/");
            var request = new RestRequest("weather");

            client.AddDefaultParameter("q", cityName, ParameterType.GetOrPost);
            client.AddDefaultParameter("appid", "06fe46f7ab5bb6942c03ad721cced379", ParameterType.GetOrPost);

            var response = await client.GetAsync(request);

            return response.Content;
        }

        public Task SendForecastData(string cityName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitConfigurations.HostName,
                Port = _rabbitConfigurations.Port,
                UserName = _rabbitConfigurations.UserName,
                Password = _rabbitConfigurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                string message = GetForecastData(cityName).Result;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                        routingKey: QUEUE_NAME,
                                        basicProperties: null,
                                        body: body);
            }
            return Task.CompletedTask;
        }
    }
}
