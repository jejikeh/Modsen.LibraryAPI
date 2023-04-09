using Modsen.Library.Application.Dtos;
using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public interface IAuthorsDataClient
{
    public Task<IEnumerable<Author>> GetAllAuthors();
    public Task<CreateAuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto);
}