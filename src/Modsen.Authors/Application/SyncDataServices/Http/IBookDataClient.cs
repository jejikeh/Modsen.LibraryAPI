using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.SyncDataServices.Http;

public interface IBookDataClient
{
    public Task SendAuthorToBook(AuthorMinDetailsDto authorMinDetailsDto);
}