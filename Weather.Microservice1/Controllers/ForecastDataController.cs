using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Microservice1.Entities;
using Weather.Microservice1.Repositories;

namespace Weather.Microservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastDataController : ControllerBase
    {
        private const string QUEUE_NAME = "ForecastRequest";
        private readonly IForecastDataRepository _repository;

        public ForecastDataController(IForecastDataRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromServices] RabbitMQConfigurations configurations, string cityName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configurations.HostName,
                Port = configurations.Port,
                UserName = configurations.UserName,
                Password = configurations.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_NAME,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var message = new { City = cityName };
                var messageJSON = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(messageJSON);

                channel.BasicPublish(exchange: "",
                                        routingKey: QUEUE_NAME,
                                        basicProperties: null,
                                        body: body);
            }

            return Ok("Mensagem encaminhada com sucesso");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForecastData>>> GetAll()
        {
            var forecasts = await _repository.GetForecasts();
            return Ok(forecasts);
        }
    }
}
