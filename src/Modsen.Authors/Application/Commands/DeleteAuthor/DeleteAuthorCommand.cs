using MediatR;

namespace Modsen.Authors.Application.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest
{
    public required Guid Id { get; set; }
}