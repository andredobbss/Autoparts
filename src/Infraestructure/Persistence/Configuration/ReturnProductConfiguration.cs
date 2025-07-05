using Autoparts.Api.Features.Returns.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class ReturnProductConfiguration : IEntityTypeConfiguration<ReturnProduct>
{
    public void Configure(EntityTypeBuilder<ReturnProduct> builder)
    {
        builder.ToTable("ReturnProducts");
        builder.HasKey(rp => new { rp.ReturnId, rp.ProductId });
        builder.Property(rp => rp.ReturnId)
            .HasColumnName("ReturnId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(rp => rp.ProductId)
            .HasColumnName("ProductId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(rp => rp.Quantity)
            .HasColumnName("Quantity")
            .IsRequired(true)
            .HasColumnType("INT")
            .HasDefaultValue(0);
    }
}
