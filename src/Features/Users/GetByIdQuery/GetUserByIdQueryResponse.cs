using Autoparts.Api.Shared.ValueObejct;

namespace Autoparts.Api.Features.Users.GetByIdQuery;

public sealed record GetUserByIdQueryResponse(Guid Id,
                                              string UserName,
                                              string Email,
                                              string TaxId,
                                              Address Address);
