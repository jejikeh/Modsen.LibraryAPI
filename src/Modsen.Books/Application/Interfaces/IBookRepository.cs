using Modsen.Books.Models;

namespace Modsen.Books.Application;

public interface IBookRepository
{
    public Task<IEnumerable<Book>> GetAllBooks();
    public Task<Book?> GetBookById(Guid id);
    public Task CreateBook(Book book);
    public Task<bool> SaveChangesAsync();
}