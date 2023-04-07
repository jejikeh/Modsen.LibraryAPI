using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetAuthorsBooks;

/// <summary>
///  Request handler for getting all books of author
/// </summary>
public class GetAuthorBooksQueryHandler : IRequestHandler<GetAuthorBooksQuery, IEnumerable<BookDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetAuthorBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDetailsDto>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllBooks();
        return _mapper.Map<IEnumerable<BookDetailsDto>>(books);
    }
}