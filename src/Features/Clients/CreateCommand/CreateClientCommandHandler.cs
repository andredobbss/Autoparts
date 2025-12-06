using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Clients.CreateCommand;
public sealed class CreateClientCommandHandler(AutopartsDbContext context) : IRequestHandler<CreateClientCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client(request.ClientName, request.Address, request.Email, request.TaxIdType, request.TaxId);

        await _context.Clients!.AddAsync(client, cancellationToken);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
        {
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        return new ValidationResult();
    }
}