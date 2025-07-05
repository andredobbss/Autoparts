using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetAllQuery;

public sealed record GetAllClientsQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<Client>>;
