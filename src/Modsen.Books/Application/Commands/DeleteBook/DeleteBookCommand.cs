using MediatR;

namespace Modsen.Books.Application.Commands.DeleteBook;

public class DeleteBookCommand : IRequest
{
    public required Guid Id { get; set; }
}