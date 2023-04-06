using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.GetAuthors;

public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDetailsDto>>
{
    
}