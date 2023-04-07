using Modsen.Authors.Services.RabbitMQ;

namespace Modsen.Authors.Services;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMessageBusClient, MessageBusClient>();
        return serviceCollection;
    }
}