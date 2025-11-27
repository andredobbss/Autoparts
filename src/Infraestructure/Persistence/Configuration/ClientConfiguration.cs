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
        builder.Property(c => c.CreatedAt).HasColumnName("CreatedAt").IsRequired(true).HasColumnType("DATETIME2");
        builder.Property(c => c.UpdatedAt).HasColumnName("UpdatedAt").IsRequired(false).HasColumnType("DATETIME2");
        builder.Property(c => c.DeletedAt).HasColumnName("DeletedAt").IsRequired(false).HasColumnType("DATETIME2");

        builder.OwnsOne(c => c.Address, AddressMappingHelper.MapAddress);

        builder.HasMany(c => c.Sales).WithOne(sales => sales.Client).HasForeignKey(sales => sales.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Returns).WithOne(returns => returns.Client).HasForeignKey(returns => returns.ClientId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.ClientId).HasDatabaseName("IX_Clients_ClientId");
        builder.HasIndex(c => c.ClientName).HasDatabaseName("IX_Clients_ClientName");
        builder.HasIndex(c => c.CreatedAt).HasDatabaseName("IX_Clients_CreatedAt");
        builder.HasIndex(c => c.DeletedAt).HasDatabaseName("IX_Clients_DeletedAt");

    }
}
