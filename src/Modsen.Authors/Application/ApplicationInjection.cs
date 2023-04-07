using System.Reflection;
using Modsen.Authors.Application.SyncDataServices.Http;

namespace Modsen.Authors.Application;

public static class ApplicationInjection
{
    
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return serviceCollection;
    }
}