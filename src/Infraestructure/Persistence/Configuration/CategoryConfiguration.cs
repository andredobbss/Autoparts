using Autoparts.Api.Features.Categories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.CategoryId);
        builder.Property(c => c.CategoryId).HasColumnName("CategoryId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(c => c.Description).HasColumnName("Description").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
        builder.Property(c => c.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(c => c.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(c => c.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.HasMany(c => c.Products).WithOne(product => product.Category).HasForeignKey(product => product.CategoryId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.CategoryId).HasDatabaseName("IX_Categories_CategoryId");
        builder.HasIndex(c => c.CreatedAt).HasDatabaseName("IX_Categories_CreatedAt");
        builder.HasIndex(c => c.DeletedAt).HasDatabaseName("IX_Categories_DeletedAt");
        builder.HasIndex(c => c.Description).IsUnique(true);
    }

}

