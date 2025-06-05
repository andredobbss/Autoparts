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
        builder.Property(p => p.TaxId).HasColumnName("TaxId").IsRequired(true).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(s => s.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(s => s.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(s => s.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.OwnsOne(s => s.Address, a =>
        {
            a.Property(p => p.Street).HasColumnName("Street").HasColumnType("NVARCHAR").HasMaxLength(255);
            a.Property(p => p.Number).HasColumnName("Number").HasColumnType("VARCHAR").HasMaxLength(20);
            a.Property(p => p.Neighborhood).HasColumnName("Neighborhood").HasColumnType("NVARCHAR").HasMaxLength(50);
            a.Property(p => p.City).HasColumnName("City").HasColumnType("NVARCHAR").HasMaxLength(30);
            a.Property(p => p.State).HasColumnName("State").HasColumnType("NVARCHAR").HasMaxLength(20);
            a.Property(p => p.Country).HasColumnName("Country").HasColumnType("NVARCHAR").HasMaxLength(20);
            a.Property(p => p.ZipCode).HasColumnName("ZipCode").HasColumnType("VARCHAR").HasMaxLength(20);
            a.Property(p => p.Complement).HasColumnName("Complement").HasColumnType("NVARCHAR").HasMaxLength(255);
            a.Property(p => p.CellPhone).HasColumnName("CellPhone").HasColumnType("VARCHAR").HasMaxLength(15);
            a.Property(p => p.Phone).HasColumnName("Phone").HasColumnType("VARCHAR").HasMaxLength(15);
        });

        builder.HasMany(s => s.Purchases).WithOne(s => s.Supplier).HasForeignKey(purchase => purchase.SupplierId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.SupplierId).HasDatabaseName("IX_Suppliers_SupplierId");
        builder.HasIndex(s => s.CreatedAt).HasDatabaseName("IX_Suppliers_CreatedAt");
        builder.HasIndex(c => c.TaxId).HasDatabaseName("IX_Suppliers_TaxId").IsUnique(true);
    }


}

