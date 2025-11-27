using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Manufacturers.CreateCommand;

public sealed class CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<CreateManufacturerCommand, ValidationResult>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<ValidationResult> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = new Manufacturer(request.Description);

        var result = await _manufacturerRepository.AddAsync(manufacturer, cancellationToken);
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