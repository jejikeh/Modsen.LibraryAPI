using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modsen.Books.Models;

namespace Modsen.Books.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasIndex(book => new { book.Id, book.AuthorId })
            .IsUnique();

        builder
            .Property(book => book.ISBN)
            .HasMaxLength(13);
        
        builder
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId);
        
    }
}