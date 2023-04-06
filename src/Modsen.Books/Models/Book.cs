namespace Modsen.Books.Models;

public class Book
{
    public required Guid Id { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required string Description { get; set; }
    public required DateTime Year { get; set; }
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }
}