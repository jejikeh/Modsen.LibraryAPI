using AutoMapper;
using MediatR;
using Modsen.Books.Application.Common.Exceptions;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDetailsDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookDetailsDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookById(request.Id);
        if(book is null)
            throw new NotFoundException<Book>(nameof(request.Id));

        var updatedBook = new Book
        {
            Id = request.Id,
            AuthorId = request.AuthorId,
            ISBN = request.ISBN ?? book.ISBN,
            Title = request.Title ?? book.Title,
            Genre = request.Genre ?? book.Genre,
            Description = request.Description ?? book.Description,
            Year = request.Year ?? book.Year,
        };

        await _bookRepository.UpdateBook(updatedBook);
        await _bookRepository.SaveChangesAsync();
        return _mapper.Map<BookDetailsDto>(updatedBook);
    }
}