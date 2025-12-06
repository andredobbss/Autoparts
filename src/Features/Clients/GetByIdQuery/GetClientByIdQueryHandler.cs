using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Clients.GetByIdQuery;

public sealed record GetClientByIdQueryHandler(AutopartsDbContext context) : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResponse>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<GetClientByIdQueryResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients!.FindAsync(request.ClientId, cancellationToken);
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