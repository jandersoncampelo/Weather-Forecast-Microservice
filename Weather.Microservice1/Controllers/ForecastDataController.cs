using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Microservice1.Entities;
using Weather.Microservice1.Repositories;

namespace Weather.Microservice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastDataController : ControllerBase
    {
        private readonly IForecastDataRepository _repository;

        public ForecastDataController(IForecastDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ForecastData>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ForecastData>>> GetForecasts()
        {
            var forecasts = await _repository.GetForecasts();
            return Ok(forecasts);
        }

        [HttpGet("{id:length(24)}", Name = "GetForecast")]
        [ProducesResponseType(typeof(ForecastData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ForecastData>> GetForecastById(string id)
        {
            var forecast = await _repository.GetForecast(id);
            if (forecast is null)
            {
                return NotFound();
            }

            return Ok(forecast);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ForecastData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ForecastData>> CreateForecast([FromBody] ForecastData forecast)
        {
            if (forecast is null)
                return BadRequest("Invalid Data");
            await _repository.CreateForecast(forecast);

            return CreatedAtRoute("GetForecast", new { id = forecast.Id }, forecast);
        }

    }
}
