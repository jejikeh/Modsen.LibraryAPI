﻿using System.Reflection;

namespace Modsen.Books.Application;

public static class ApplicationInjection
{
    
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return serviceCollection;
    }
}