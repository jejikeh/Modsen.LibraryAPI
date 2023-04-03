using AutoMapper;
using MediatR;
using Modsen.Authors.Application.Interfaces;

namespace Modsen.Authors.Application.Commands.GetAllAuthors;

public class GetAllAuthorsMinQueryHandler : IRequestHandler<GetAllAuthorsMinQuery, IEnumerable<AuthorReadMinDto>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAllAuthorsMinQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorReadMinDto>> Handle(GetAllAuthorsMinQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAuthors();
        return _mapper.Map<IEnumerable<AuthorReadMinDto>>(authors);
    }
}