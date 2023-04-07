using Modsen.Books.Services.RabbitMQProcessing;
using Modsen.Books.Services.RabbitMQSubscriber;

namespace Modsen.Books.Services;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        Console.WriteLine("Injecting Services");
        serviceCollection.AddSingleton<IEventProcessor, EventProcessor>();
        serviceCollection.AddHostedService<MessageBusSubscriber>();
        return serviceCollection;
    }
}