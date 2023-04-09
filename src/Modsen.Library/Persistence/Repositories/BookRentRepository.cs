using Microsoft.EntityFrameworkCore;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Persistence.Repositories;

public class BookRentRepository : IBookRentRepository
{
    private readonly LibraryDbContext _libraryDbContext;

    public BookRentRepository(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    public async Task<IEnumerable<BookRent>> GetAllRents()
    {
        return await _libraryDbContext.BookRents.ToListAsync();
    }

    public async Task<BookRent?> GetBookRentById(Guid id)
    {
        return await _libraryDbContext.BookRents.FirstOrDefaultAsync(bookRent => bookRent.Id == id);
    }

    public async Task<BookRent?> GetUserBookRentById(Guid userId, Guid id)
    {
        return await _libraryDbContext.BookRents.FirstOrDefaultAsync(bookRent => bookRent.Id == id && bookRent.UserId == userId);
    }

    public IEnumerable<BookRent> GetAllUserRents(Guid userId)
    {
        return _libraryDbContext.BookRents.Where(rent => rent.UserId == userId);
    }

    public async Task<bool> RentExist(Guid id)
    {
        return await _libraryDbContext.BookRents.AnyAsync(rent => rent.Id == id);
    }

    public async Task AddRent(BookRent bookRent)
    {
        await _libraryDbContext.BookRents.AddAsync(bookRent);
    }

    public async Task UpdateRent(BookRent bookRent)
    {
        var updatedRent = await GetBookRentById(bookRent.Id);
        if (updatedRent is null)
            return;

        updatedRent.IsActive = bookRent.IsActive;
        updatedRent.EndRent = bookRent.EndRent;
    }

    public async Task DeleteRent(Guid id)
    {
        var rent = await GetBookRentById(id);
        if (rent is null)
            return;

        _libraryDbContext.BookRents.Remove(rent);
    }

    public async Task SaveChangesAsync()
    {
        await _libraryDbContext.SaveChangesAsync();
    }
}