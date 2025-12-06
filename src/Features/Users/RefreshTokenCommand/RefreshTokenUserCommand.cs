using Autoparts.Api.Features.Users.DTOs;
using MediatR;

namespace Autoparts.Api.Features.Users.RefreshTokenCommand;

public sealed record RefreshTokenUserCommand(string AccessToken,
                                             string RefreshToken) : IRequest<Token>;

