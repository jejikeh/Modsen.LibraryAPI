﻿using MediatR;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
{
    private readonly IAuthorRepository _authorRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            Bio = request.Bio,
            BornDate = request.Born,
            DieDate = request.Die,
            FirstName = request.FirstName,
            Id = Guid.NewGuid(),
            LastName = request.LastName
        };

        
        await _authorRepository.CreateAuthor(author);
        await _authorRepository.SaveChangesAsync();
        return author;
    }
}