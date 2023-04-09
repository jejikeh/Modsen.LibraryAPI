using Modsen.Library.Models;

namespace Modsen.Library.Application.Interfaces;

public interface IBookRentRepository
{
    public Task<IEnumerable<BookRent>> GetAllRents();
    public Task<BookRent?> GetBookRentById(Guid id);
    public Task<BookRent?> GetUserBookRentById(Guid userId, Guid id);
    public IEnumerable<BookRent> GetAllUserRents(Guid userId);
    public Task<bool> RentExist(Guid id);
    public Task AddRent(BookRent bookRent);
    public Task UpdateRent(BookRent bookRent);
    public Task DeleteRent(Guid id);
    public Task SaveChangesAsync();
}