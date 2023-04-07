using MediatR;
using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Application.Commands.UpdateAuthor;

public class UpdateAuthorCommand : IRequest<AuthorDetailsDto>
{
    public required Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Born { get; set; }
    public DateTime? Die { get; set; }
    public string? Bio { get; set; }
}