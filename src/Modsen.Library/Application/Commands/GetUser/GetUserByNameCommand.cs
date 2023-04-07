using MediatR;
using Modsen.Library.Application.Dtos;

namespace Modsen.Library.Application.Commands.GetUser;

public class GetUserByIdCommand : IRequest<UserDetailsDto>
{
    public required Guid Id { get; set; }
}