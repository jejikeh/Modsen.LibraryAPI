using MediatR;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<Author>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Born { get; set; }
    public DateTime? Die { get; set; }
    public string? Bio { get; set; }
}