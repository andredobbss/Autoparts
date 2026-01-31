using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Clients.DeleteCommand;
public sealed class DeleteClientCommandHandler(AutopartsDbContext context) : IRequestHandler<DeleteClientCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await context.Clients!.FindAsync(request.ClientId, cancellationToken);
        if (client is null)
            return new ValidationResult([new ValidationFailure(Resource.CLIENT, string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId))]);

        client.Delete();

        context.Clients!.Update(client);

        var commitResult = await context.SaveChangesAsync(cancellationToken);
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