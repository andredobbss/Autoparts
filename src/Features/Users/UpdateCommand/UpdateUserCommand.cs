using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.ValueObejct;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.UpdateCommand;

public sealed record UpdateUserCommand(Guid Id,
                                       string UserName,
                                       string Email,
                                       string Password,
                                       ETaxIdType? TaxIdType,
                                       string? TaxId,
                                       Address Address) : IRequest<IdentityResult>;