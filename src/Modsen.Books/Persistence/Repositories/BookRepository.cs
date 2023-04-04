using Microsoft.EntityFrameworkCore;
using Modsen.Books.Application;
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

    public async Task<Book?> GetBookById(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task CreateBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        await _context.AddAsync(book);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}