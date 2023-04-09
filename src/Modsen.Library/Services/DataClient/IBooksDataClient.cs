using Modsen.Library.Application.Dtos;
using Modsen.Library.Models;

namespace Modsen.Library.Services.DataClient;

public interface IBooksDataClient
{
    public Task<IEnumerable<Book>> GetAllBooks();
    public Task<Book> GetBookByISBN(string isbn);
    public Task<CreateBookDto> CreateBook(CreateBookDto book);
    public Task<IEnumerable<AuthorMinimalDto>> GetAllBookAuthors();
}