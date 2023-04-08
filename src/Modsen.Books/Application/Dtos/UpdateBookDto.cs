namespace Modsen.Books.Application.Dtos;

public class UpdateBookDto
{
    public string? ISBN { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public DateTime? Year { get; set; }
}