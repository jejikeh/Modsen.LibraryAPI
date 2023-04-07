using MediatR;
using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Application.Commands.GetAuthors;

public class GetAuthorsQuery : IRequest<IEnumerable<AuthorDetailsDto>>
{
    
}