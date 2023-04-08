using System.Collections;
using System.Collections.Immutable;
using System.Text.Json;
using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public class AuthorDataClient : IAuthorDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthorDataClient> _logger;


    public AuthorDataClient(HttpClient httpClient, IConfiguration configuration, ILogger<AuthorDataClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        var request = await _httpClient.GetAsync(_configuration.GetServiceUri("modsen-authors") + "api/Authors/GetAllAuthors");
        if (request.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync Get request to author service was successful");
            var authors =
                await JsonSerializer.DeserializeAsync<IEnumerable<Author>>(await request.Content.ReadAsStreamAsync(), new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            
            if (authors is not null) 
                return authors;
            
            _logger.LogError("Failed to parse data from author service");
            throw new NullReferenceException(nameof(authors));
        }
        
        _logger.LogError("Sync Get request to author service was not successful");
        return ImmutableArray<Author>.Empty;
    }
}