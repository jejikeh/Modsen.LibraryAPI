using AutoMapper;
using MediatR;
using Modsen.Authors.Application.Dtos;
using Modsen.Authors.Application.Interfaces;

namespace Modsen.Authors.Application.Commands.GetAuthors;

public class GetAuthorsMinQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorMinDetailsDto>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsMinQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorMinDetailsDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthors();
        return _mapper.Map<IEnumerable<AuthorMinDetailsDto>>(authors);
    }
}