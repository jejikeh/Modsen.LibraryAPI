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

    public async Task AddBookToAuthor(Guid authorId, Book book)
    {
        var author = await GetAuthorById(authorId);
        author?.Books.Add(book);
    }

    public async Task DeleteAuthorByExternalId(Guid externalId)
    {
        var author = await GetAuthorByExternalId(externalId);
        if (author is null)
            return;
        
        _context.Authors.Remove(author);
    }

    public async Task UpdateAuthor(Author author)
    {
        var updateAuthor = await GetAuthorByExternalId(author.ExternalId);
        if (updateAuthor is null)
            return;

        updateAuthor.Name = author.Name;
    }

    private async Task<Author?> GetAuthorByExternalId(Guid externalId)
    {
        return await _context.Authors.FirstOrDefaultAsync(author => author.ExternalId == externalId);
    }
    
    public async Task<Author?> GetAuthorById(Guid id)
    {
        return await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
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