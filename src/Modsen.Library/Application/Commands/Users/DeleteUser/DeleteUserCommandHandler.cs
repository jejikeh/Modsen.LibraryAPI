using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.Users.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.UserId))
            throw new NotFoundException<User>(nameof(request.UserId));
        
        await _userRepository.DeleteUser(request.UserId);
        await _userRepository.SaveChangesAsync();
    }
}