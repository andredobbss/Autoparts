using Autoparts.Api.Features.Manufacturers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.ToTable("Manufacturers");
        builder.HasKey(m => m.ManufacturerId);
        builder.Property(m => m.ManufacturerId).HasColumnName("ManufacturerId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(m => m.Description).HasColumnName("Description").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
        builder.Property(m => m.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(m => m.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(m => m.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.HasMany(m => m.Products).WithOne(product => product.Manufacturer).HasForeignKey(product => product.ManufacturerId).OnDelete(DeleteBehavior.Restrict);
   
        builder.HasIndex(m => m.ManufacturerId).HasDatabaseName("IX_Manufacturers_ManufacturerId");
        builder.HasIndex(m => m.CreatedAt).HasDatabaseName("IX_Manufacturers_CreatedAt");
        builder.HasIndex(m => m.DeletedAt).HasDatabaseName("IX_Manufacturers_DeletedAt");
        builder.HasIndex(m => m.Description).IsUnique(true);

    }
}
