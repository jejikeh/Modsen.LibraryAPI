using MediatR;
using Modsen.Library.Models;

namespace Modsen.Library.Application.Commands.BookRents.GetAllBookRents;

public class GetBookRentsCommand : IRequest<IEnumerable<BookRent>>
{
    
}