using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Application.SyncDataServices.Http;

public interface IBookDataClient
{
    public Task SendAuthorToBook(AuthorDetailsDto authorMinDetailsDto);
}