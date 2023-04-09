using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.CreateBookRent;

public class CreateBookRentCommand : IRequest<BookRent>
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public int RentLength { get; set; }
}