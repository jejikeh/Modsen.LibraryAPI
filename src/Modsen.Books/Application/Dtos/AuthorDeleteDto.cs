namespace Modsen.Books.Application.Dtos;

public class AuthorDeleteDto
{
    public required Guid Id { get; set; }
    public required string Event { get; set; }
}