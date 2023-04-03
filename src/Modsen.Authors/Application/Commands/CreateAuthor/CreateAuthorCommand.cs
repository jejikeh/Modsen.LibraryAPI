using MediatR;

namespace Modsen.Authors.Application.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Born { get; set; }
    public string? Bio { get; set; }
    public DateTime? Die { get; set; }
}