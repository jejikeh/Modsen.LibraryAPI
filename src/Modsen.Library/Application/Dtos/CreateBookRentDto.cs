namespace Modsen.Library.Application.Dtos;

public class CreateBookRentDto
{
    public Guid BookId { get; set; }
    public int RentLength { get; set; }
}