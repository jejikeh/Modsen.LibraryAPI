using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;

namespace Modsen.Books.Application.Commands.GetBooks;

public class GetBooksMinQueryHandler : IRequestHandler<GetBooksMinQuery, IEnumerable<BookMinDetailsDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksMinQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookMinDetailsDto>> Handle(GetBooksMinQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllBooks();
        return _mapper.Map<IEnumerable<BookMinDetailsDto>>(books);
    }
}