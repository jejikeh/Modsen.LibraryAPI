using Modsen.Books.Models;

namespace Modsen.Books.Application.Interfaces;

public interface IAuthorRepository
{
    public Task<IEnumerable<Author>> GetAllAuthors();
    public Task<Author> CreateAuthor(Author author);
    public Task<bool> AuthorExist(Guid authorId);
    public Task<bool> ExternalAuthorExist(Guid externalAuthorId);
}