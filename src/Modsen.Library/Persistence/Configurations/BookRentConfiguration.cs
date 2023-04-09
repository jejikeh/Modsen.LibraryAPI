using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modsen.Library.Models;

namespace Modsen.Library.Persistence.Configurations;

public class BookRentConfiguration : IEntityTypeConfiguration<BookRent>
{
    public void Configure(EntityTypeBuilder<BookRent> builder)
    {
        builder.HasKey(rent => rent.Id);
        builder.HasIndex(rent => new { rent.UserId, rent.BookId });
        builder.Property(rent => rent.StartRent).IsRequired();
        builder.Property(rent => rent.EndRent).IsRequired();
    }
}