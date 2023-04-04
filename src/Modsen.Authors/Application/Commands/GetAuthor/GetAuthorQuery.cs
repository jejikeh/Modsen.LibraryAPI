using MediatR;

namespace Modsen.Authors.Application.Commands.GetAuthor;

public class GetAuthorQuery : IRequest<AuthorDetailsDto>
{
    public required Guid Id { get; set; }
}