using Microsoft.EntityFrameworkCore;
using Modsen.Authors.Models;

namespace Modsen.Authors.Persistence;

public class AuthorDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }

    public AuthorDbContext(DbContextOptions<AuthorDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
    } 
}