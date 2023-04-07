using Microsoft.EntityFrameworkCore;

namespace Modsen.Library.Persistence;

public class UserDbContext : DbContext
{
    public DbSet<User>
}