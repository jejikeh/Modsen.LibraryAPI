namespace Modsen.Library.Models;

public class BookRent
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateOnly StartRent { get; set; }
    public DateOnly EndRent { get; set; }
    public bool IsActive { get; set; } = true;
}