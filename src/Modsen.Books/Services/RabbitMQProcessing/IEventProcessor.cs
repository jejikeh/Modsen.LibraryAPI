namespace Modsen.Books.Services.EventProcessing;

public interface IEventProcessor
{
    public void ProcessEvent(string message);
}