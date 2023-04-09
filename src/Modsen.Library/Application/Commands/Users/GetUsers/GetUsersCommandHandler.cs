using AutoMapper;
using MediatR;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Application.Interfaces;

namespace Modsen.Library.Application.Commands.Users.GetUsers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, IEnumerable<UserDetailsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDetailsDto>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserDetailsDto>>(users);
    }
}