using Microsoft.EntityFrameworkCore;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _context.Books.ToListAsync();
    }

    public IEnumerable<Book> GetAllAuthorBooks(Guid authorId)
    {
        return _context.Books
            .Where(book => book.AuthorId == authorId)
            .OrderBy(book => book.Title);
    }

    public async Task<Book?> GetBookById(Guid bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);
    }
    
    public async Task<Book?> GetBookByAuthorIdAndBookId(Guid authorId, Guid bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.AuthorId == authorId && book.Id == bookId);
    }

    public async Task<Book?> GetBookByISBN(string isbn)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.ISBN == isbn);
    }

    public async Task UpdateBook(Book book)
    {
        var updateBook = await GetBookById(book.Id);
        if (updateBook is null)
            return;

        updateBook.ISBN = book.ISBN;
        updateBook.Title = book.Title;
        updateBook.Description = book.Description;
        updateBook.Genre = book.Genre;
        updateBook.Year = book.Year;
    }

    public async Task DeleteBook(Guid id)
    {
        var book = await GetBookById(id);
        if (book is null)
            return;

        _context.Remove(book);
    }

    public async Task CreateBook(Guid authorId, Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        book.AuthorId = authorId;
        await _context.Books.AddAsync(book);
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}