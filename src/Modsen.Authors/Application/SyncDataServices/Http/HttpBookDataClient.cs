using System.Text;
using System.Text.Json;
using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.SyncDataServices.Http;

public class HttpBookDataClient : IBookDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpBookDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task SendAuthorToBook(AuthorMinDetailsDto authorMinDetailsDto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(authorMinDetailsDto),
            Encoding.UTF8,
            "application/json");

        // TODO: inject logger here
        var response = await _httpClient.PostAsync($"{_configuration.GetServiceUri("modsen-books")}/api/external/authors", httpContent);
        Console.WriteLine(response.IsSuccessStatusCode
            ? "--> Sync Post to Book Service was Ok"
            : "--> Sync Post to Book Service was Not Ok");
    }
}