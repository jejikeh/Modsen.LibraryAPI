using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetBook;

public class GetAuthorBookDetailsQuery : IRequest<BookDetailsDto>
{
    public required Guid Id { get; set; }
    public required Guid AuthorId { get; set; }
}