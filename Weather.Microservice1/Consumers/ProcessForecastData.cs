using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Weather.Microservice1.Entities;
using Weather.Microservice1.Repositories;

namespace Weather.Microservice1.Consumers
{
    public class ProcessForecastData : BackgroundService
    {
        private const string QUEUE = "ForecastData";
        private readonly IForecastDataRepository _repository;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public ProcessForecastData(IServiceProvider serviceProvider)
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
                var forecastDataJson = Encoding.UTF8.GetString(byteArray);

                var forecastData = new ForecastData
                {
                    Data = forecastDataJson
                };

                PersistForecastData(forecastData);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public void PersistForecastData(ForecastData data)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repositoryService = scope.ServiceProvider.GetRequiredService<IForecastDataRepository>();
                repositoryService.CreateForecast(data);
            }
        }
    }
}
