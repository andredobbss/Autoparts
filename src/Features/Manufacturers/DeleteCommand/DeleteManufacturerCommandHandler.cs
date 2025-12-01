using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;

public sealed class DeleteManufacturerCommandHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<DeleteManufacturerCommand, ValidationResult>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;

    public async Task<ValidationResult> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            return new ValidationResult([new ValidationFailure(Resource.MANUFACTORER, string.Format(Resource.MANUFACTORER_NOT_FOUND, request.ManufacturerId))]);

        manufacturer.Delete();

        var deleted = await _manufacturerRepository.DeleteAsync(manufacturer, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.MANUFACTORER, Resource.FAILED_TO_DELETE_MANUFACTORER)]);

        var committed = await _manufacturerRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}