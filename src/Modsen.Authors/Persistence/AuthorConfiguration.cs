using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modsen.Authors.Models;

namespace Modsen.Authors.Persistence;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.Id);
        builder.Property(author => author.FirstName).HasMaxLength(280);
        builder.Property(author => author.LastName).HasMaxLength(280);
    }
}