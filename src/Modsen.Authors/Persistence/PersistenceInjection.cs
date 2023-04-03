using Microsoft.EntityFrameworkCore;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Persistence.Repositories;

namespace Modsen.Authors.Persistence;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AuthorDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-library-db"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }, 
            ServiceLifetime.Transient
        )
        /*

        */
        serviceCollection.AddScoped<IAuthorRepository, AuthorRepository>();
        return serviceCollection;
    } 
}