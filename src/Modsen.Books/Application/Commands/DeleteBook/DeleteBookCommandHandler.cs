using MediatR;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        if(await _bookRepository.GetBookById(request.Id) is null)
            throw new NotFoundException<Book>(nameof(request.Id));

        await _bookRepository.DeleteBook(request.Id);
        await _bookRepository.SaveChangesAsync();
    }
}