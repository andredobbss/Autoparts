namespace Autoparts.Api.Shared.ValueObjects;

public sealed record Address(
string Street,
string Number,
string Neighborhood,
string City,
string State,
string Country,
string ZipCode,
string Complement,
string CellPhone,
string Phone);
