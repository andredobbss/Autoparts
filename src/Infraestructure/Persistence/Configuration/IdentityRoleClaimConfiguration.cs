using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("IdentityRoleClaim");
        builder.HasKey(rc => rc.Id);
        builder.Property(rc => rc.RoleId);
        builder.Property(rc => rc.ClaimType).HasMaxLength(256);
        builder.Property(rc => rc.ClaimValue).HasMaxLength(256);
    }
}
