using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("IdentityUserLogin");
        builder.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        builder.Property(ul => ul.LoginProvider).HasMaxLength(128);
        builder.Property(ul => ul.ProviderKey);
        builder.Property(builder => builder.ProviderDisplayName).HasMaxLength(256);
    }
}
