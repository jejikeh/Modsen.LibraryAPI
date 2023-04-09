using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.GetUser;

public class GetUserByNameCommandHandler : IRequestHandler<GetUserByNameCommand, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByNameCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByName(request.Name);
        if (user is null)
            throw new NotFoundException<User>(nameof(request.Name));
        
        return user;
    }
}