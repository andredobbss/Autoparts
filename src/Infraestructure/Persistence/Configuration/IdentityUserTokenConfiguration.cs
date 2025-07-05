using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable ("IdentityUserToken");

        builder.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
        builder.Property(token => token.LoginProvider).IsRequired().HasMaxLength(128);
        builder.Property(token => token.Name).IsRequired().HasMaxLength(128);
    }
}
