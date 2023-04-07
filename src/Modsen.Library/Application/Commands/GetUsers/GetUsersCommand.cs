using MediatR;
using Modsen.Library.Application.Dtos;

namespace Modsen.Library.Application.Commands.GetUsers;

public class GetUsersCommand : IRequest<IEnumerable<UserDetailsDto>>
{
    
}