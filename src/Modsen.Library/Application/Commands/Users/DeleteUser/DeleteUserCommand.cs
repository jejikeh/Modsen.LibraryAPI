using MediatR;

namespace Modsen.Library.Application.Commands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public required Guid UserId { get; set; }
}