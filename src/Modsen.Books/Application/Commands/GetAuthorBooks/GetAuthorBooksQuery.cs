using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetAuthorBooks;

public class GetAuthorBooksQuery : IRequest<IEnumerable<BookDetailsDto>>
{
    public required Guid AuthorId { get; set; }
}