using MediatR;
using Modsen.Library.Application.Dtos;

namespace Modsen.Library.Application.Commands.Users.UpdateUser;

public class UpdateUserCommand : IRequest<UserDetailsDto>
{
    public required Guid Id { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Password { get; set; }
}