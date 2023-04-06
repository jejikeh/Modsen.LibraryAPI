using MediatR;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Commands.CreateBook;

public class CreateBookCommand : IRequest<Book>
{
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required string Description { get; set; }
    public required DateTime Year { get; set; }
    public required Guid AuthorId { get; set; }
}