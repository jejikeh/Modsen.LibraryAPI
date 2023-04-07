using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.GetAuthorBooks;

public class GetAuthorBooksQueryHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<BookDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorBooksQueryHandler(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<BookDetailsDto>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
    {
        if (!await _authorRepository.AuthorExist(request.AuthorId))
            throw new NotFoundException<Author>(nameof(request.AuthorId));
        
        var books = _bookRepository.GetAllAuthorBooks(request.AuthorId);
        return _mapper.Map<IEnumerable<BookDetailsDto>>(books);
    }
}