using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.UpdateBookRent;

public class UpdateBookRentCommand : IRequest<BookRent>
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public bool? IsActive { get; set; }
    public DateOnly? EndRent { get; set; }
}