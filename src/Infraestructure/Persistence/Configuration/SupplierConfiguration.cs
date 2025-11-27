using Autoparts.Api.Features.Suppliers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");
        builder.HasKey(s => s.SupplierId);
        builder.Property(s => s.SupplierId).HasColumnName("SupplierId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(s => s.CompanyName).HasColumnName("CompanyName").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(255);
        builder.Property(s => s.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(s => s.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(s => s.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.OwnsOne(s => s.Address, AddressMappingHelper.MapAddress);

        builder.HasMany(s => s.Purchases).WithOne(s => s.Supplier).HasForeignKey(purchase => purchase.SupplierId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.SupplierId).HasDatabaseName("IX_Suppliers_SupplierId");
        builder.HasIndex(s => s.CreatedAt).HasDatabaseName("IX_Suppliers_CreatedAt");
        builder.HasIndex(s => s.DeletedAt).HasDatabaseName("IX_Suppliers_DeletedAt");
    }


}

