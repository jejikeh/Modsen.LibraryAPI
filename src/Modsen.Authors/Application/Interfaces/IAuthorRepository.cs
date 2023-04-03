using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Interfaces;

public interface IAuthorRepository
{
    public Task<IEnumerable<Author>> GetAllAuthors();
    public Task<Author?> GetAuthorById(Guid id);
    public Task CreateAuthor(Author author);
    public Task<bool> SaveChangesAsync();
}