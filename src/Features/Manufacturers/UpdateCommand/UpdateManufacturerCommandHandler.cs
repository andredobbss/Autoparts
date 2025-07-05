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
        return result;
    }
}