namespace Modsen.Library.Application.Dtos;

public class CreateAuthorDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Born { get; set; }
    public DateTime? Die { get; set; }
    public string? Bio { get; set; }
}