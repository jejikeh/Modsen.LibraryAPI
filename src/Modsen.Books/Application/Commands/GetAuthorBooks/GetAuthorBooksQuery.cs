using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetAuthorsBooks;

public class GetAuthorBooksQuery : IRequest<IEnumerable<BookDetailsDto>>
{
    
}