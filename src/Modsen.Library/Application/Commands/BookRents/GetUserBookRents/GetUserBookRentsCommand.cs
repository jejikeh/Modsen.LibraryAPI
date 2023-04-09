using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetUserBookRents;

public class GetUserBookRentsCommand : IRequest<IEnumerable<BookRent>>
{
    public required Guid UserId { get; set; }
}