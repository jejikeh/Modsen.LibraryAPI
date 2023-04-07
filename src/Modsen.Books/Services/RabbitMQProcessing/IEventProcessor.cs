namespace Modsen.Books.Services.RabbitMQProcessing;

public interface IEventProcessor
{
    public void ProcessEvent(string message);
}