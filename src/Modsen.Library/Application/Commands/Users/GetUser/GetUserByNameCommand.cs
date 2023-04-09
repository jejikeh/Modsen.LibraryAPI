using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.Users.GetUser;

public class GetUserByNameCommand : IRequest<User>
{
    public required string Name { get; set; }
}