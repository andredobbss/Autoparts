using Microsoft.EntityFrameworkCore;
using Autoparts.Api.Features.Functionary.Domain;

namespace Autoparts.Api.Features.Functionary.Infraestructure;

public class FunctionaryDbContext : DbContext
{
    public FunctionaryDbContext(DbContextOptions<FunctionaryDbContext> options) : base(options)
    {
    }

    public DbSet<FunctionaryDomain> Functionaries { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FunctionaryDomain>(entity =>
        {
            entity.ToTable("Functionaries");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            // Add other properties and configurations as needed
        });

    }
}


