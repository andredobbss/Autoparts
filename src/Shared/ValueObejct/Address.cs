using Autoparts.Api.Shared.Resources;
using FluentValidation;
using FluentValidation.Results;

namespace Autoparts.Api.Shared.ValueObejct;

public sealed record Address
{
    //private readonly AddressValidator _addressValidation = new();

    public Address(string? street, string? number, string? neighborhood, string? city, string? state, string? country, string? zipCode, string? complement, string? cellPhone, string taxId)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Complement = complement;
        CellPhone = cellPhone;
        TaxId = taxId;

        //if (AddressResult().IsValid is false)
        //    throw new ValidationException(Resource.ERROR_DOMAIN, AddressResult().Errors);
    }

    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? Neighborhood { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
    public string? Complement { get; init; }
    public string? CellPhone { get; init; }
    public string? TaxId { get; init; }

    //private ValidationResult AddressResult()
    //{
    //    return _addressValidation.Validate(this);
    //}
}
