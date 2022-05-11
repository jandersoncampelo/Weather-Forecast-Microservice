using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Wheater.Microservice2.Services;

namespace Wheater.Microservice2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _weatherForecastService.GetForecastData("London");
            return Ok("Mensagem encaminhada com sucesso");
        }
    }
}
