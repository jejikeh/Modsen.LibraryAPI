using AutoMapper;
using MediatR;
using Modsen.Authors.Application.Dtos;
using Modsen.Authors.Application.Interfaces;

namespace Modsen.Authors.Application.Commands.GetAuthor;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorDetailsDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<AuthorDetailsDto> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAuthorById(request.Id);
        return _mapper.Map<AuthorDetailsDto>(authors);
    }
}