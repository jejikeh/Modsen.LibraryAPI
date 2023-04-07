using AutoMapper;
using MediatR;
using Modsen.Books.Application.Commands.GetBook;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetAuthorBook;

public class GetBookQueryHandler : IRequestHandler<GetAuthorBookDetailsQuery, BookDetailsDto>
{
    private readonly IAppRepository _appRepository;
    private readonly IMapper _mapper;

    public GetBookQueryHandler(IAppRepository appRepository, IMapper mapper)
    {
        _appRepository = appRepository;
        _mapper = mapper;
    }

    public async Task<BookDetailsDto> Handle(GetAuthorBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await _appRepository.GetBookById(request.Id);
        return _mapper.Map<BookDetailsDto>(book);
    }
}