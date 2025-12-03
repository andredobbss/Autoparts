namespace Autoparts.Api.Shared.ValueObejct;

public sealed record Address
{
    public Address(string? street,
                   string? number,
                   string? neighborhood,
                   string? city,
                   string? state,
                   string? country,
                   string? zipCode,
                   string? complement,
                   string? cellPhone)
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
    }

    public string? Street { get; private set; }
    public string? Number { get; private set; }
    public string? Neighborhood { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? ZipCode { get; private set; }
    public string? Complement { get; private set; }
    public string? CellPhone { get; private set; }
}
