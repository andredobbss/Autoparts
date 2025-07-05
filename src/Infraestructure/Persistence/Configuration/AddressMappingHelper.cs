using Autoparts.Api.Shared.ValueObejct;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoparts.Api.Infraestructure.Persistence.Configuration;

public static class AddressMappingHelper
{
    public static void MapAddress<T>(OwnedNavigationBuilder<T, Address> address) where T : class
    {
       address.Property(a => a.Street).HasMaxLength(255);
       address.Property(a => a.Number).HasMaxLength(20);
       address.Property(a => a.Neighborhood).HasMaxLength(50);
       address.Property(a => a.City).HasMaxLength(30);
       address.Property(a => a.State).HasMaxLength(20);
       address.Property(a => a.Country).HasMaxLength(20);
       address.Property(a => a.ZipCode).HasMaxLength(20);
       address.Property(a => a.TaxId).HasMaxLength(15);
       address.Property(a => a.Complement).HasMaxLength(255);
       address.Property(a => a.CellPhone).HasMaxLength(15);
    }
}