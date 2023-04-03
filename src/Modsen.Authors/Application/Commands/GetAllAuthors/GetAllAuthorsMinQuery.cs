using MediatR;

namespace Modsen.Authors.Application.Commands.GetAllAuthors;

public class GetAllAuthorsMinQuery : IRequest<IEnumerable<AuthorReadMinDto>>
{
    
}