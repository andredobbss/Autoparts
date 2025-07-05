using Autoparts.Api.Features.Users.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("Id").HasColumnType("UNIQUEIDENTIFIER");
        builder.Property(u => u.Email).HasColumnName("Email").HasColumnType("VARCHAR").HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasColumnName("NormalizedEmail").HasColumnType("VARCHAR").HasMaxLength(256);
        builder.Property(u => u.UserName ).HasColumnName("UserName").HasColumnType("NVARCHAR").HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasColumnName("NormalizedUserName").HasColumnType("NVARCHAR").HasMaxLength(256);
        builder.Property(u => u.PhoneNumber).HasColumnType("VARCHAR").HasMaxLength(20);
        builder.Property(u => u.EmailConfirmed).HasColumnType("BIT").HasDefaultValue(true);
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();



        builder.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        builder.HasMany<IdentityRoleClaim<Guid>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        builder.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        builder.OwnsOne(u => u.Address, AddressMappingHelper.MapAddress);

        builder.HasMany(u => u.Purchases).WithOne(p => p.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Sales).WithOne(s => s.User).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.Returns).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("IX_IdentityUser_NormalizedUserName").IsUnique(true);
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("IX_IdentityUser_NormalizedEmail").IsUnique(true);

    }
}
