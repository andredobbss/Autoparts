namespace Autoparts.Api.Features.Users.DTOs;

public sealed record Token(string AccessToken,
                                  string RefreshToken,
                                  DateTime? RefreshTokenExpiryTime);

