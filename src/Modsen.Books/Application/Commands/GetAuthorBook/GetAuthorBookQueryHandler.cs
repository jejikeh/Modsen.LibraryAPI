using AutoMapper;
using MediatR;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.GetAuthorBook;

public class GetAuthorBookQueryHandler : IRequestHandler<GetAuthorBookDetailsQuery, BookDetailsDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAuthorBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookDetailsDto> Handle(GetAuthorBookDetailsQuery request, CancellationToken cancellationToken)
    {
        if (!await _bookRepository.AuthorExist(request.AuthorId))
            throw new NotFoundException<Author>(nameof(request.AuthorId));
        
        var book = await _bookRepository.GetBookById(request.AuthorId, request.BookId);
        
        if(book is null)
            throw new NotFoundException<Book>(nameof(request.BookId));

        return _mapper.Map<BookDetailsDto>(book);
    }
}