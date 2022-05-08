namespace Weather.Microservice1.Services
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}
