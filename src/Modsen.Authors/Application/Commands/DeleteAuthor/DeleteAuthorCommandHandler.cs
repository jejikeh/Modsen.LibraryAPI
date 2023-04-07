using MediatR;
using Modsen.Authors.Application.Common.Exceptions;
using Modsen.Authors.Application.Interfaces;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        if (!await _authorRepository.AuthorExist(request.Id))
            throw new NotFoundException<Author>(nameof(request.Id));
        
        await _authorRepository.DeleteAuthor(request.Id);
        await _authorRepository.SaveChangesAsync();
    }
}