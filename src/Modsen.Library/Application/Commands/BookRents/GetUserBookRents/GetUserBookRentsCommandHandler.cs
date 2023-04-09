using MediatR;
using Modsen.Library.Application.Common.Exceptions;
using Modsen.Library.Application.Interfaces;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetUserBookRents;

public class GetUserBookRentsCommandHandler : IRequestHandler<GetUserBookRentsCommand, IEnumerable<BookRent>>
{
    private readonly IBookRentRepository _bookRentRepository;
    private readonly IUserRepository _userRepository;
    
    public GetUserBookRentsCommandHandler(IBookRentRepository bookRentRepository, IUserRepository userRepository)
    {
        _bookRentRepository = bookRentRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<BookRent>> Handle(GetUserBookRentsCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.UserExist(request.UserId))
            throw new NotFoundException<User>(nameof(request.UserId));
        
        return _bookRentRepository.GetAllUserRents(request.UserId);
    }
}