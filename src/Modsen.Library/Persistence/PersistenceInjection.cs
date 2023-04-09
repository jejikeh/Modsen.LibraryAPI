using Microsoft.EntityFrameworkCore;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;
using Modsen.Library.Persistence.Repositories;

namespace Modsen.Library.Persistence;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<LibraryDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-library-dev") + "Database=modsen_library_dev;");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            },
            ServiceLifetime.Transient
        );
        
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IBookRentRepository, BookRentRepository>();
        return serviceCollection;
    } 
}
