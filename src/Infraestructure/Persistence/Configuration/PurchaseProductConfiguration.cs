using Autoparts.Api.Features.Purchases.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class PurchaseProductConfiguration : IEntityTypeConfiguration<PurchaseProduct>
{
    public void Configure(EntityTypeBuilder<PurchaseProduct> builder)
    {
        builder.ToTable("PurchaseProducts");
        builder.HasKey(pp => new { pp.PurchaseId, pp.ProductId });
        builder.Property(pp => pp.PurchaseId)
            .HasColumnName("PurchaseId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(pp => pp.ProductId)
            .HasColumnName("ProductId")
            .IsRequired(true)
            .HasColumnType("UNIQUEIDENTIFIER");     
        builder.Property(pp => pp.Quantity)
            .HasColumnName("Quantity")
            .IsRequired(true)
            .HasColumnType("INT")
            .HasDefaultValue(0);
        builder.Property(pp => pp.AcquisitionCost)
            .HasColumnName("AcquisitionCost")
            .IsRequired(true)
            .HasColumnType("DECIMAL(18,2)")
            .HasDefaultValue(0m);
        builder.Property(pp => pp.TotalItem)
            .HasColumnName("TotalItem")
            .HasColumnType("DECIMAL(18,2)")
            .HasDefaultValue(0m);
    }
}
