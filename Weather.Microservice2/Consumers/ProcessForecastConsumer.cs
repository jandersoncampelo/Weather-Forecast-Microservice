using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Weather.Microservice2.Models;
using Wheater.Microservice2.Services;

namespace Weather.Microservice2.Consumers
{
    public class ProcessForecastConsumer : BackgroundService
    {
        private const string QUEUE = "ForecastRequest";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public ProcessForecastConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var jsonRequest = Encoding.UTF8.GetString(byteArray);

                var forecastFor = JsonSerializer.Deserialize<ForecastRequestModel>(jsonRequest);

                SendForecastData(forecastFor.City);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public void SendForecastData(string cityName)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repositoryService = scope.ServiceProvider.GetRequiredService<IWeatherForecastService>();
                repositoryService.SendForecastData(cityName);
            }
        }
    }
}
