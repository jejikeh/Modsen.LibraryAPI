using AutoMapper;
using MediatR;
using Modsen.Authors.Application.Common.Exceptions;
using Modsen.Authors.Application.Dtos;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDetailsDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<AuthorDetailsDto> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        if(!await _authorRepository.AuthorExist(request.Id))
            throw new NotFoundException<Author>(nameof(request.Id));

        var author = await _authorRepository.GetAuthorById(request.Id);
        var updatedAuthor = new Author
        {
            Id = request.Id,
            FirstName = request.FirstName ?? author.FirstName,
            LastName = request.LastName ?? author.LastName,
            BornDate = request.Born ?? author.BornDate,
            Bio = request.Bio ?? author.Bio,
            DieDate = request.Die ?? author.DieDate
        };

        await _authorRepository.UpdateAuthor(updatedAuthor);
        await _authorRepository.SaveChangesAsync();
        return _mapper.Map<AuthorDetailsDto>(updatedAuthor);
    }
}