using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public interface IAuthorDataClient
{
    public Task<IEnumerable<Author>> GetAllAuthors();
}