namespace Modsen.Library.Application.Dtos;

public class UserLoginDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}