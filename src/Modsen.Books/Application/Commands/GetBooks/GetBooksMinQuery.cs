using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetBooks;

public class GetBooksMinQuery : IRequest<IEnumerable<BookMinDetailsDto>>
{
    
}