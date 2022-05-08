namespace Weather.Microservice1.Services
{
    public class ForecastDataService : IForecastDataService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "ForecastData";

        public ForecastDataService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessWeatherForecast()
        {
            _messageBusService.Publish(QUEUE_NAME, "");
        }
    }
}
