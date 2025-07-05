using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
       builder.ToTable("IdentityRole");
       builder.HasKey(r => r.Id);
       builder.Property(r => r.Name).HasMaxLength(256);
       builder.Property(r => r.NormalizedName).HasMaxLength(256);
         builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken().HasMaxLength(256);

        builder.HasIndex(r => r.NormalizedName).IsUnique();
    }
}
