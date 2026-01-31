using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;

public sealed class DeleteManufacturerCommandHandler(AutopartsDbContext context) : IRequestHandler<DeleteManufacturerCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await context.Manufacturers!.FindAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            return new ValidationResult([new ValidationFailure(Resource.MANUFACTORER, string.Format(Resource.MANUFACTORER_NOT_FOUND, request.ManufacturerId))]);

        manufacturer.Delete();

        context.Manufacturers!.Update(manufacturer);

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