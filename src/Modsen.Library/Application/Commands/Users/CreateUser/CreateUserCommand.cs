using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<User>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
}