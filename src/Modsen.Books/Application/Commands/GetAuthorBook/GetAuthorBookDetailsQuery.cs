using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetAuthorBook;

public class GetAuthorBookDetailsQuery : IRequest<BookDetailsDto>
{
    public required Guid BookId { get; set; }
    public required Guid AuthorId { get; set; }
}