using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.UpdateBookRent;

public class UpdateBookRentCommandHandler : IRequestHandler<UpdateBookRentCommand, BookRent>
{
    private readonly IBookRentRepository _bookRentRepository;

    public UpdateBookRentCommandHandler(IBookRentRepository bookRentRepository)
    {
        _bookRentRepository = bookRentRepository;
    }

    public async Task<BookRent> Handle(UpdateBookRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _bookRentRepository.GetUserBookRentById(request.UserId, request.Id);
        if(rent is null)
            throw new NotFoundException<BookRent>(nameof(request.Id));

        var updateRent = new BookRent()
        {
            BookId = rent.BookId,
            EndRent = request.EndRent ?? rent.EndRent,
            Id = rent.Id,
            IsActive = request.IsActive ?? rent.IsActive,
            StartRent = rent.StartRent,
            UserId = rent.UserId
        };

        await _bookRentRepository.UpdateRent(updateRent);
        return updateRent;
    }
}