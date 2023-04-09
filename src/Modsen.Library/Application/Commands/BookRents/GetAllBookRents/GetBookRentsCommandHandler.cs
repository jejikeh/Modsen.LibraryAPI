using MediatR;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetAllBookRents;

public class GetBookRentsCommandHandler : IRequestHandler<GetBookRentsCommand, IEnumerable<BookRent>>
{
    private readonly IBookRentRepository _bookRentRepository;

    public GetBookRentsCommandHandler(IBookRentRepository bookRentRepository)
    {
        _bookRentRepository = bookRentRepository;
    }

    public async Task<IEnumerable<BookRent>> Handle(GetBookRentsCommand request, CancellationToken cancellationToken)
    {
        return await _bookRentRepository.GetAllRents();
    }
}