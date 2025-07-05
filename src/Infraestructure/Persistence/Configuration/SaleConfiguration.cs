using Autoparts.Api.Features.Sales.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.SaleId);
        builder.Property(s => s.SaleId).HasColumnName("SaleId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(s => s.InvoiceNumber).HasColumnName("InvoiceNumber").IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(50);
        builder.Property(s => s.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(s => s.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(s => s.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(s => s.UserId).HasColumnName("UserId").IsRequired(true);
        builder.Property(s => s.ClientId).HasColumnName("ClientId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");

        builder.HasMany(s => s.Products).WithMany(p => p.Sales)
            .UsingEntity<SaleProduct>(
              j => j.HasOne(sp => sp.Product)
                  .WithMany(p => p.SaleProducts)
                  .HasForeignKey(sp => sp.ProductId),
              j => j.HasOne(sp => sp.Sale)
                  .WithMany(s => s.SaleProducts)
                  .HasForeignKey(sp => sp.SaleId));
           
        builder.HasIndex(s => s.SaleId).HasDatabaseName("IX_Sales_SaleId");
        builder.HasIndex(s => s.CreatedAt).HasDatabaseName("IX_Sales_CreatedAt");
    }

}

