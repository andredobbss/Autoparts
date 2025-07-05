using Autoparts.Api.Features.Returns.Domain;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQuery(Guid ReturnId) : IRequest<Return>;
