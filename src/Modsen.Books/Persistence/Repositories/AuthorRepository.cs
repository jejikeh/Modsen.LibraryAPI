using Microsoft.EntityFrameworkCore;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _context;

    public AuthorRepository(BookDbContext bookDbContext)
    {
        _context = bookDbContext;
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
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}