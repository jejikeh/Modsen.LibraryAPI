using AutoMapper;
using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailsDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.Id))
            throw new NotFoundException<User>(nameof(request.Id));
        
        var user = await _userRepository.GetUserById(request.Id);

        var updatedUser = new User()
        {
            Id = request.Id,
            FirstName = request.FirstName ?? user.FirstName,
            LastName = request.LastName ?? user.LastName,
            PasswordHash = request.Password is null
                ? user.PasswordHash
                : BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _userRepository.UpdateUser(updatedUser);
        return _mapper.Map<UserDetailsDto>(updatedUser);
    }
}