using Modsen.Books.Models;

namespace Modsen.Books.Application.Interfaces;

public interface IBookRepository
{
    public Task<IEnumerable<Book>> GetAllBooks();
    public IEnumerable<Book> GetAllAuthorBooks(Guid authorId);
    public Task<Book?> GetBookById(Guid authorId, Guid bookId);
    public Task<Book?> GetBookByISBN(string isbn);
    public Task CreateBook(Guid authorId, Book book);
    public Task<bool> SaveChangesAsync();
}