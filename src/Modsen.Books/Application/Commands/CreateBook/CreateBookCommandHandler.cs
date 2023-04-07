using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDetailsDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<BookDetailsDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            ISBN = request.ISBN,
            Title = request.Title,
            Genre = request.Genre,
            Description = request.Description,
            Year = request.Year,
        };

        await _bookRepository.CreateBook(request.AuthorId, book);
        await _authorRepository.AddBookToAuthor(request.AuthorId, book);
        await _bookRepository.SaveChangesAsync();
        return _mapper.Map<BookDetailsDto>(book);
    }
}