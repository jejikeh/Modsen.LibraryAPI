namespace Modsen.Library.Application.Dtos;

public class CreateBookDto
{
    public required Guid AuthorId { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required string Description { get; set; }
    public required DateTime Year { get; set; }
}