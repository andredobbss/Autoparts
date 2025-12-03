using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.Domain;

public sealed class User : IdentityUser<Guid>
{
    public User() { }

    public ETaxIdType? TaxIdType { get; private set; }
    public string? TaxId { get; private set; }

    public Address Address { get; private set; } = null!;


    public ICollection<IdentityRole<Guid>>? Roles { get; private set; } = [];
    public ICollection<Purchase> Purchases { get; private set; } = [];
    public ICollection<Sale> Sales { get; private set; } = [];
    public ICollection<Return> Returns { get; private set; } = [];

}
