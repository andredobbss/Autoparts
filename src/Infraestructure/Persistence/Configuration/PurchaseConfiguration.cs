using Autoparts.Api.Features.Purchases.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("Purchases");
        builder.HasKey(p => p.PurchaseId);
        builder.Property(p => p.PurchaseId).HasColumnName("PurchaseId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(p => p.InvoiceNumber).HasColumnName("InvoiceNumber").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(50);
        builder.Property(p => p.TotalPurchase).HasColumnName("TotalPurchase").IsRequired(true).HasColumnType("DECIMAL(18,2)");
        builder.Property(p => p.PaymentMethod).HasColumnName("PaymentMethod").IsRequired(true).HasColumnType("INT");
        builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(p => p.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired(true);

        //builder.HasMany(p => p.Products)
        //    .WithMany(products => products.Purchases)
        //    .UsingEntity(j => j.ToTable("PurchaseProducts"));

        builder.HasMany(pu => pu.Products)
               .WithMany(pr => pr.Purchases)
               .UsingEntity<PurchaseProduct>(
                   j => j
                       .HasOne(pp => pp.Product)
                       .WithMany(p => p.PurchaseProducts)
                       .HasForeignKey(pp => pp.ProductId),
                   j => j
                       .HasOne(pp => pp.Purchase)
                       .WithMany(p => p.PurchaseProducts)
                       .HasForeignKey(pp => pp.PurchaseId));

        builder.HasIndex(p => p.PurchaseId).HasDatabaseName("IX_Purchases_PurchaseId");
        builder.HasIndex(p => p.InvoiceNumber).HasDatabaseName("IX_Purchases_InvoiceNumber");
        builder.HasIndex(p => p.CreatedAt).HasDatabaseName("IX_Purchases_CreatedAt");
        builder.HasIndex(p => p.DeletedAt).HasDatabaseName("IX_Purchases_DeletedAt");

    }
}
