using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.UpdateCommand;
public sealed class UpdateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<UpdateManufacturerCommand, ValidationResult>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;

    public async Task<ValidationResult> Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            return new ValidationResult { Errors = { new ValidationFailure("Manufacturer", $"{Resource.ID_NOT_FOUND} : {request.ManufacturerId}") } };

        manufacturer.Update(request.Description);

        var result = await _manufacturerRepository.UpdateAsync(manufacturer, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _manufacturerRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}