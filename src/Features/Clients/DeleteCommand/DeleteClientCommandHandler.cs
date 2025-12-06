using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Clients.DeleteCommand;
public sealed class DeleteClientCommandHandler(AutopartsDbContext context) : IRequestHandler<DeleteClientCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients!.FindAsync(request.ClientId, cancellationToken);
        if (client is null)
            return new ValidationResult([new ValidationFailure(Resource.CLIENT, string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId))]);

        client.Delete();

        _context.Clients!.Update(client);

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