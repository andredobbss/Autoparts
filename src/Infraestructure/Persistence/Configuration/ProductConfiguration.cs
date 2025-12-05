using Autoparts.Api.Features.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", p =>
        {
            p.IsTemporal(t =>
            {
                t.UseHistoryTable("ProductsHistory");
                t.HasPeriodStart("SysStartTime").HasColumnName("SysStartTime");
                t.HasPeriodEnd("SysEndTime").HasColumnName("SysEndTime");
            });
        });
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.ProductId).HasColumnName("ProductId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(p => p.TechnicalDescription).HasColumnName("TechnicalDescription").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(255);
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
        builder.Property(p => p.SKU).HasColumnName("SKU").IsRequired(true).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(p => p.Compatibility).HasColumnName("Compatibility").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(50);
        builder.Property(p => p.AcquisitionCost).HasColumnName("AcquisitionCost").IsRequired(true).HasColumnType("DECIMAL(18,2)").HasDefaultValue(0.0m);
        builder.Property(p => p.SellingPrice).HasColumnName("SellingPrice").IsRequired(true).HasColumnType("DECIMAL(18,2)").HasDefaultValue(0.0m);
        builder.Property(p => p.Stock).HasColumnName("Stock").IsRequired(true).HasColumnType("INT").HasDefaultValue(0);
        builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(p => p.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(p => p.CategoryId).HasColumnName("CategoryId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(p => p.ManufacturerId).HasColumnName("ManufacturerId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");

        // indexes
        builder.HasIndex(p => p.ProductId).HasDatabaseName("IX_Products_ProductId");
        builder.HasIndex(p => p.CreatedAt).HasDatabaseName("IX_Products_CreatedAt");
        builder.HasIndex(p => p.DeletedAt).HasDatabaseName("IX_Products_DeletedAt");
        builder.HasIndex(p => p.SKU).HasDatabaseName("IX_Products_SKU").IsUnique(true);

        // Ignore properties not mapped to database columns
        builder.Ignore(p => p.Quantity);
        builder.Ignore(p => p.StockStatus);
        builder.Ignore(p => p.StockStatusOverTime);
    }
}
