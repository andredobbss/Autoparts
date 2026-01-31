using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Manufacturers.UpdateCommand;
public sealed class UpdateManufacturerCommandHandler(AutopartsDbContext context) : IRequestHandler<UpdateManufacturerCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await context.Manufacturers!.FindAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            return new ValidationResult { Errors = { new ValidationFailure(Resource.MANUFACTORER, string.Format(Resource.MANUFACTORER_NOT_FOUND, request.ManufacturerId)) } };

        manufacturer.Update(request.Description);

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