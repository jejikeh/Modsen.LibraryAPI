using AutoMapper;
using MediatR;
using Modsen.Library.Application.Commands.GetUser;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.Users.GetUser;

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDetailsDto> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.Id))
            throw new NotFoundException<User>(nameof(request.Id));

        var user = await _userRepository.GetUserById(request.Id);
        return _mapper.Map<UserDetailsDto>(user);
    }
}