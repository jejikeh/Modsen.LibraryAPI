using MediatR;
using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Application.Commands.GetAuthor;

public class GetAuthorQuery : IRequest<AuthorDetailsDto>
{
    public required Guid Id { get; set; }
}