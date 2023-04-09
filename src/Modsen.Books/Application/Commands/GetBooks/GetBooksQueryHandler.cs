using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetBooks;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDetailsDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllBooks();
        return _mapper.Map<IEnumerable<BookDetailsDto>>(books);
    }
}