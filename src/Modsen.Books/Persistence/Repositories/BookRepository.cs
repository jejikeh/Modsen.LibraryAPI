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

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));

        await _context.Authors.AddAsync(author);
        return author;
    }

    public async Task<bool> AuthorExist(Guid authorId)
    {
        return await _context.Authors.AnyAsync(author => author.Id == authorId);
    }

    public async Task<bool> ExternalAuthorExist(Guid externalAuthorId)
    {
        return await _context.Authors.AnyAsync(author => author.ExternalId == externalAuthorId);
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

    public async Task<Book?> GetBookById(Guid authorId, Guid bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.AuthorId == authorId && book.Id == bookId);
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