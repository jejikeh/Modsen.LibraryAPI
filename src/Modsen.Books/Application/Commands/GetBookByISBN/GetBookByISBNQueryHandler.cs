using AutoMapper;
using MediatR;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.GetBookByISBN;

public class GetBookByISBNQueryHandler : IRequestHandler<GetBookByISBNQuery, BookDetailsDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByISBNQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookDetailsDto> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByISBN(request.ISBN);
        if(book is null)
            throw new NotFoundException<Book>(nameof(request.ISBN));

        return _mapper.Map<BookDetailsDto>(book);
    }
}