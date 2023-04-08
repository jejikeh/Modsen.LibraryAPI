using System.Reflection;
using Modsen.Authors.Application;
using Modsen.Authors.Application.Common.Mappings;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Persistence;
using Modsen.Authors.Services;

namespace Modsen.Authors.Extensions;

public static class ServiceMiddlewareExtensions
{
    public static WebApplicationBuilder RegisterServiceMiddleware(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });
        
        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IAuthorRepository).Assembly));
        });
        
        builder.Services
            .AddServices()
            .AddApplication()
            .AddPersistence(builder.Configuration);
        
        builder.Services.AddCors(options =>
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            }));
        
        return builder;
    }
    
        public static WebApplication InitializeServiceContextProvider(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var authorDbContext = serviceProvider.GetRequiredService<AuthorDbContext>();
                authorDbContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //
            }

            return app;
        }
}