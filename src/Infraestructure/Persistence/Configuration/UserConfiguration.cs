using Autoparts.Api.Features.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.TaxId).HasColumnName("TaxId").IsRequired(true).HasColumnType("VARCHAR").HasMaxLength(20);

        builder.OwnsOne(u => u.Address, a =>
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

        builder.HasMany(u => u.Purchases).WithOne(p => p.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Sales).WithOne(s => s.User).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Returns).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.TaxId).HasDatabaseName("IX_Users_TaxId").IsUnique(true);

    }
}
