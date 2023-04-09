namespace Modsen.Library.Application.Dtos;

public class UpdateBookRentDto
{
    public required Guid Id { get; set; }
    public bool? IsActive { get; set; }
    public DateOnly? EndRent { get; set; }
}