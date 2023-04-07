using Microsoft.EntityFrameworkCore;
using Modsen.Books.Application;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Persistence.Repositories;

namespace Modsen.Books.Persistence;

public static class PersistenceInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BookDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("modsen-library-dev"));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            },
            ServiceLifetime.Transient
        );
        
        serviceCollection.AddScoped<IBookRepository, BookRepository>();
        serviceCollection.AddScoped<IAuthorRepository, AuthorRepository>();

        return serviceCollection;
    } 
}