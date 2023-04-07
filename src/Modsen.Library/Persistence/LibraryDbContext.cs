using Microsoft.EntityFrameworkCore;
using Modsen.Library.Models;
using Modsen.Library.Persistence.Configurations;

namespace Modsen.Library.Persistence;

public class LibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}