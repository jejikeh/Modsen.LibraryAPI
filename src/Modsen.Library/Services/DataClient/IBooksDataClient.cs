using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public interface IBooksClient
{
    public Task<IEnumerable<Book>> GetAllBooks();
}