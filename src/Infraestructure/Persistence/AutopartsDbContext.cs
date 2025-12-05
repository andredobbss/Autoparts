using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Infraestructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Infraestructure.Persistence;

public sealed class AutopartsDbContext : IdentityDbContext<User,
                                                           IdentityRole<Guid>,
                                                           Guid,
                                                           IdentityUserClaim<Guid>,
                                                           IdentityUserRole<Guid>,
                                                           IdentityUserLogin<Guid>,
                                                           IdentityRoleClaim<Guid>,
                                                           IdentityUserToken<Guid>>

{
    public AutopartsDbContext(DbContextOptions<AutopartsDbContext> options) : base(options) { }

    public DbSet<Category>? Categories { get; set; } = null;
    public DbSet<Client>? Clients { get; set; } = null;
    public DbSet<Manufacturer>? Manufacturers { get; set; } = null;
    public DbSet<Product>? Products { get; set; } = null;
    public DbSet<Purchase>? Purchases { get; set; } = null;
    public DbSet<Return>? Returns { get; set; } = null;
    public DbSet<Sale>? Sales { get; set; } = null;
    public DbSet<Supplier>? Suppliers { get; set; } = null;
 
    // Intermediary Entities
    public DbSet<PurchaseProduct>? PurchaseProducts { get; set; } = null;
    public DbSet<SaleProduct>? SaleProducts { get; set; } = null;
    public DbSet<ReturnProduct>? ReturnProducts { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityRoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityUserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityUserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new IdentityUserTokenConfiguration());
        modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        // Intermediary Entities
        modelBuilder.ApplyConfiguration(new PurchaseProductConfiguration());
        modelBuilder.ApplyConfiguration(new ReturnProductConfiguration());
        modelBuilder.ApplyConfiguration(new SaleProductConfiguration());

        // filters for soft delete
        modelBuilder.Entity<Category>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Client>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Manufacturer>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Product>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Purchase>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Return>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Sale>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<Supplier>().HasQueryFilter(c => c.DeletedAt == null);
        modelBuilder.Entity<User>().HasQueryFilter(c => c.IsActive);
    }
}
