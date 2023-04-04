using MediatR;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            ISBN = request.ISBN,
            Title = request.Title,
            Genre = request.Author,
            Description = request.Description,
            Year = request.Year,
            Author = request.Author
        };

        await _bookRepository.CreateBook(book);
        await _bookRepository.SaveChangesAsync();
        return book;
    }
}