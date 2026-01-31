using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResponse>
{
    public async Task<GetClientByIdQueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await context.Clients!.FindAsync(request.ClientId, cancellationToken);
        if (client is null)
            throw new KeyNotFoundException(string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId));

        return new GetClientByIdQueryResponse
        (
             client.ClientId,
             client.ClientName,
             client.Email,
             client.TaxIdType,
             client.TaxId,
             client.CreatedAt,
             client.Address
        );

    }
}