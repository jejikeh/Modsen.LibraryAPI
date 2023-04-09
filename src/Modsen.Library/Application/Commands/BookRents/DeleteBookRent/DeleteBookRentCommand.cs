using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.DeleteBookRent;

public class DeleteBookRentCommand : IRequest
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
}