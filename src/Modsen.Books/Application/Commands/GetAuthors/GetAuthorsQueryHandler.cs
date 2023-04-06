using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetAuthors;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAuthorsQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDetailsDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _bookRepository.GetAllAuthors();
        return _mapper.Map<IEnumerable<AuthorDetailsDto>>(authors);
    }
}