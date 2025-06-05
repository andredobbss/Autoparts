using Autoparts.Api.Features.Returns.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class ReturnConfiguration : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.ToTable("Returns");
        builder.HasKey(r => r.ReturnId);
        builder.Property(r => r.ReturnId).HasColumnName("ReturnId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(r => r.Justification).HasColumnName("Justification").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(255);
        builder.Property(r => r.InvoiceNumber).HasColumnName("InvoiceNumber").IsRequired(false).HasColumnType("NVARCHAR").HasMaxLength(50);
        builder.Property(r => r.Quantity).HasColumnName("Quantity").IsRequired(true).HasColumnType("INT").HasDefaultValue(0);
        builder.Property(r => r.Loss).HasColumnName("Loss").IsRequired(true).HasColumnType("BIT").HasDefaultValue(false);
        builder.Property(r => r.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(r => r.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(r => r.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired(true);
        builder.Property(r => r.ClientId).HasColumnName("ClientId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");

        //builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
        //builder.HasOne(r => r.Client).WithMany().HasForeignKey(r => r.ClientId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Products)
            .WithMany(p => p.Returns)
            .UsingEntity(j => j.ToTable("ReturnProducts"));

        builder.HasIndex(r => r.ReturnId).HasDatabaseName("IX_Returns_ReturnId");
        builder.HasIndex(r => r.CreatedAt).HasDatabaseName("IX_Returns_CreatedAt");

    }
}
