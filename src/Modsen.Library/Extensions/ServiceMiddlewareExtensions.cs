using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Modsen.Library.Application;
using Modsen.Library.Application.Common.Mappings;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Configuration;
using Modsen.Library.Persistence;
using Modsen.Library.Services;
using Swashbuckle.AspNetCore.Filters;

namespace Modsen.Library.Extensions;

public static class ServiceMiddlewareExtensions
{
    public static WebApplicationBuilder RegisterServiceMiddleware(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Modsen Library",
                Description = "An ASP.NET Web api for library service"
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        
        builder.Services
            .AddServices()
            .AddApplication()
            .AddPersistence(builder.Configuration);
        
        builder.Services
            .AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = AuthConfiguration.GetSymmetricSecurityKeyStatic()
                };
            });

        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IUserRepository).Assembly));
        });
        
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
            var authorDbContext = serviceProvider.GetRequiredService<LibraryDbContext>();
            authorDbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            //
        }

        return app;
    }
}