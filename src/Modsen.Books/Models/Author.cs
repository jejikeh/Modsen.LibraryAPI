namespace Modsen.Books.Models;

public class Author
{
    public required Guid Id { get; set; }
    public required Guid ExternalId { get; set; }
    public required string Name { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}