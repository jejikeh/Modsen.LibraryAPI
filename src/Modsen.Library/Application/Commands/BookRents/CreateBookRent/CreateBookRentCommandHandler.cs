using MediatR;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.CreateBookRent;

public class CreateBookRentCommandHandler : IRequestHandler<CreateBookRentCommand, BookRent>
{
    private readonly IBookRentRepository _bookRentRepository;

    public CreateBookRentCommandHandler(IBookRentRepository bookRentRepository)
    {
        _bookRentRepository = bookRentRepository;
    }

    public async Task<BookRent> Handle(CreateBookRentCommand request, CancellationToken cancellationToken)
    {
        var bookRent = new BookRent()
        {
            Id = Guid.NewGuid(),
            BookId = request.BookId,
            EndRent = DateOnly.FromDateTime(DateTime.Today).AddDays(request.RentLength),
            StartRent = DateOnly.FromDateTime(DateTime.Today),
            UserId = request.UserId
        };

        await _bookRentRepository.AddRent(bookRent);
        await _bookRentRepository.SaveChangesAsync();
        return bookRent;
    }
}