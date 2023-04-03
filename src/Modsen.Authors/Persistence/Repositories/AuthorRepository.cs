using Microsoft.EntityFrameworkCore;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Models;

namespace Modsen.Authors.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AuthorDbContext _context;
    
    public AuthorRepository(AuthorDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorById(Guid id)
    {
        return await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task CreateAuthor(Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));

        await _context.AddAsync(author);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}