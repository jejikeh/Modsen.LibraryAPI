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

    public async Task<bool> AuthorExist(Guid id)
    {
        return await _context.Authors.AnyAsync(author => author.Id == id);
    }

    public async Task CreateAuthor(Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));

        await _context.AddAsync(author);
    }

    public async Task DeleteAuthor(Guid id)
    {
        var user = await GetAuthorById(id);
        if (user is null)
            return;
                
        _context.Authors.Remove(user);
    }

    public async Task UpdateAuthor(Author author)
    {
        var updateAuthor = await GetAuthorById(author.Id);
        if (updateAuthor is null)
            return;

        updateAuthor.Bio = author.Bio;
        updateAuthor.BornDate = author.BornDate;
        updateAuthor.DieDate = author.DieDate;
        updateAuthor.FirstName = author.FirstName;
        updateAuthor.LastName = author.LastName;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}