using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("IdentityUserClaim");
        builder.HasKey(uc => uc.Id);
        builder.Property(uc => uc.ClaimType).HasColumnType("VARCHAR").HasMaxLength(100);
        builder.Property(uc => uc.ClaimValue).HasColumnType("VARCHAR").HasMaxLength(100);
    }
}
