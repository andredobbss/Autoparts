using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.Domain;

public sealed class User : IdentityUser<Guid>
{
    private readonly UserValidator _userValidator = new();

    public User() { }

    public ETaxIdType? TaxIdType { get; private set; }
    public string? TaxId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; } = null;
    public bool IsActive { get; private set; } = true;

    // Refresh Token properties
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    // Navigation properties
    public Address Address { get; private set; } = null!;
    public ICollection<IdentityRole<Guid>>? Roles { get; private set; } = [];
    public ICollection<Purchase> Purchases { get; private set; } = [];
    public ICollection<Sale> Sales { get; private set; } = [];
    public ICollection<Return> Returns { get; private set; } = [];

    public User(
                string userName,
                string email,
                string password,
                ETaxIdType? taxIdType,
                string? taxId,
                Address address)
    {
        var user = this;
        user.UserName = userName;
        user.Email = email;
        user.PasswordHash = password;
        user.TaxIdType = taxIdType;
        user.TaxId = taxId;
        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = true;
        user.Address = address;
        user.PhoneNumber = address.CellPhone;

        UserResult();
    }

    public void Update(
                string userName,
                string email,
                string password,
                ETaxIdType? taxIdType,
                string? taxId,
                Address address)
    {
        var user = this;
        user.UserName = userName;
        user.Email = email;
        user.PasswordHash = password;
        user.TaxIdType = taxIdType;
        user.TaxId = taxId;
        user.UpdatedAt = DateTime.UtcNow;
        user.Address = address;
        user.PhoneNumber = address.CellPhone;

        UserResult();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        UpdatedAt = DateTime.UtcNow;
    }

    private void UserResult()
    {
        var result = _userValidator.Validate(this);

        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

}
