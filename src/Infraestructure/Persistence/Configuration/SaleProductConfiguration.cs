using Autoparts.Api.Features.Sales.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder.ToTable("SaleProducts");
        builder.HasKey(sp => new { sp.SaleId, sp.ProductId });
        builder.Property(sp => sp.SaleId)
            .HasColumnName("SaleId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(sp => sp.ProductId)
            .HasColumnName("ProductId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(sp => sp.Quantity)
            .HasColumnName("Quantity")
            .IsRequired(true)
            .HasColumnType("INT")
            .HasDefaultValue(0);
    }
}
