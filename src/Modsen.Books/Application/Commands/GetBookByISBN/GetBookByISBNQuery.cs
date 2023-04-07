using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetBookByISBN;

public class GetBookByISBNQuery : IRequest<BookDetailsDto>
{
    public string ISBN { get; set; }
}