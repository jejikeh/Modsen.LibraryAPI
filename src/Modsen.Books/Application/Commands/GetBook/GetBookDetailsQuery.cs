using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetBook;

public class GetBookQuery : IRequest<BookDetailsDto>
{
    public required Guid Id { get; set; }
}