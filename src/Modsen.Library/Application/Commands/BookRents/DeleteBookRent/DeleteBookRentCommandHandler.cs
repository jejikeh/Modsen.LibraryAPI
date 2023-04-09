using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.DeleteBookRent;

public class DeleteBookRentCommandHandler : IRequestHandler<DeleteBookRentCommand>
{
    private readonly IBookRentRepository _bookRentRepository;

    public DeleteBookRentCommandHandler(IBookRentRepository bookRentRepository)
    {
        _bookRentRepository = bookRentRepository;
    }

    public async Task Handle(DeleteBookRentCommand request, CancellationToken cancellationToken)
    {
        if(await _bookRentRepository.GetUserBookRentById(request.UserId, request.Id) is null)
            throw new NotFoundException<BookRent>(nameof(request.Id));

        await _bookRentRepository.DeleteRent(request.Id);
        await _bookRentRepository.SaveChangesAsync();
    }
}