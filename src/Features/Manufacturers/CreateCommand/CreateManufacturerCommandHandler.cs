using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.CreateCommand;

public sealed class CreateManufacturerCommandHandler(AutopartsDbContext context) : IRequestHandler<CreateManufacturerCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = new Manufacturer(request.Description);

        await _context.Manufacturers!.AddAsync(manufacturer, cancellationToken);

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