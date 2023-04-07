using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetAuthors;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorsQueryHandler(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<AuthorDetailsDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthors();
        return _mapper.Map<IEnumerable<AuthorDetailsDto>>(authors);
    }
}