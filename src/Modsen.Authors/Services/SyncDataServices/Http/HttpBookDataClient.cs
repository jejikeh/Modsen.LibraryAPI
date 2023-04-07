using System.Text;
using System.Text.Json;
using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Application.SyncDataServices.Http;

public class HttpBookDataClient : IBookDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpBookDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task SendAuthorToBook(AuthorDetailsDto authorDetailsDto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(authorDetailsDto),
            Encoding.UTF8,
            "application/json");

        // TODO: inject logger here
        var response = await _httpClient.PostAsync($"{_configuration["BookService"]}/authors/", httpContent);
        Console.WriteLine(response.IsSuccessStatusCode
            ? "--> Sync Post to Book Service was Ok"
            : "--> Sync Post to Book Service was Not Ok");
    }
}