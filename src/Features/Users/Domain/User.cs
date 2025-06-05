using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.Domain;

public sealed class User : IdentityUser
{
    private User() { }

    public IReadOnlyCollection<Purchase> Purchases { get; private set; } = []; //ok
    public IReadOnlyCollection<Sale> Sales { get; private set; } = []; //ok
    public IReadOnlyCollection<Return> Returns { get; private set; } = []; //ok
    public string TaxId { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    public User(Address address)
    {
        Address = address;
    }

    public void Update(Address address)
    {
        Address = address;
    }




}
