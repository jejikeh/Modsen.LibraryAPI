using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetBookRent;

public class GetBookRentCommand : IRequest<BookRent>
{
    public required Guid Id { get; set; }
}