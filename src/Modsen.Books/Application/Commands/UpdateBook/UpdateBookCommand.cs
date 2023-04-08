using MediatR;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Application.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<BookDetailsDto>
{
    public required Guid Id { get; set; }
    public required Guid AuthorId { get; set; }
    public string? ISBN { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public DateTime? Year { get; set; }
}