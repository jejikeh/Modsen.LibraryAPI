using System.Collections.Immutable;
using System.Text.Json;
using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public class BooksDataDataClient : IBooksDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<BooksDataDataClient> _logger;
    
    public BooksDataDataClient(HttpClient httpClient, IConfiguration configuration, ILogger<BooksDataDataClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        var request = await _httpClient.GetAsync(_configuration.GetServiceUri("modsen-books") + "api/Books");
        if (request.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sync Get books to books service was successful");
            var books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(await request.Content.ReadAsStreamAsync(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (books is not null)
                return books;
            _logger.LogError("Failed to parse data from books service");
            throw new NullReferenceException(nameof(Book));
        }
        
        _logger.LogError("Sync Get request to books service was not successful");
        return ImmutableArray<Book>.Empty;
    }
}