using Microsoft.EntityFrameworkCore;
using Modsen.Books.Models;
using Modsen.Books.Persistence.Configurations;

namespace Modsen.Books.Persistence;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}