namespace Modsen.Authors.Application.Dtos;

public class UpdateAuthorDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Born { get; set; }
    public DateTime? Die { get; set; }
    public string? Bio { get; set; }
}