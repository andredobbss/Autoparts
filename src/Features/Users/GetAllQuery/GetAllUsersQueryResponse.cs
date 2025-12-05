using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Users.GetAllQuery;

public sealed record GetAllUsersQueryResponse(Guid Id,
                                              string UserName,
                                              string Email,
                                              string TaxId,
                                              Address Address);
