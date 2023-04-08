using Modsen.Books.Models;

namespace Modsen.Books.Application.Interfaces;

public interface IBookRepository
{
    public Task<IEnumerable<Book>> GetAllBooks();
    public IEnumerable<Book> GetAllAuthorBooks(Guid authorId);
    public Task<Book?> GetBookById(Guid bookId);
    public Task<Book?> GetBookByAuthorIdAndBookId(Guid authorId, Guid bookId);
    public Task<Book?> GetBookByISBN(string isbn);
    public Task UpdateBook(Book book);
    public Task DeleteBook(Guid id);
    public Task CreateBook(Guid authorId, Book book);
    public Task<bool> SaveChangesAsync();
}