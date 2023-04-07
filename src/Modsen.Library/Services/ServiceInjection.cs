using Modsen.Library.Services.DataClient;

namespace Modsen.Library.Services;

public static class ServiceInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IAuthorDataClient, AuthorDataClient>();
        return serviceCollection;
    }
}