using Autoparts.Api.Features.Clients.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.ClientId);
        builder.Property(c => c.ClientId).HasColumnName("ClientId").IsRequired(true).HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(c => c.ClientName).HasColumnName("ClientName").IsRequired(true).HasColumnType("NVARCHAR").HasMaxLength(100);
        builder.Property(p => p.TaxId).HasColumnName("TaxId").IsRequired(true).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(c => c.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(c => c.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(c => c.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.OwnsOne(c => c.Address, a =>
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

        builder.HasMany(c => c.Sales).WithOne(sales => sales.Client).HasForeignKey(sales => sales.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Returns).WithOne(returns => returns.Client).HasForeignKey(returns => returns.ClientId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.ClientId).HasDatabaseName("IX_Clients_ClientId");
        builder.HasIndex(c => c.ClientName).HasDatabaseName("IX_Clients_ClientName");
        builder.HasIndex(c => c.CreatedAt).HasDatabaseName("IX_Clients_CreatedAt");
        builder.HasIndex(c => c.TaxId).HasDatabaseName("IX_Clients_TaxId").IsUnique(true);

    }
}
