using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetBookRent;

public class GetBookRentCommandHandler : IRequestHandler<GetBookRentCommand, BookRent>
{
    private readonly IBookRentRepository _bookRentRepository;

    public GetBookRentCommandHandler(IBookRentRepository bookRentRepository)
    {
        _bookRentRepository = bookRentRepository;
    }

    public async Task<BookRent> Handle(GetBookRentCommand request, CancellationToken cancellationToken)
    {
        if(!await _bookRentRepository.RentExist(request.Id))
            throw new NotFoundException<BookRent>(nameof(request.Id));

        var rent = await _bookRentRepository.GetBookRentById(request.Id);
        return rent;
    }
}