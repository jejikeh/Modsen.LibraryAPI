namespace Modsen.Books.Models;

public class Author
{
    public required Guid Id { get; set; }
    public required Guid ExternalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BornDate { get; set; }
    public DateTime? DieDate { get; set; }
    public string? Bio { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}