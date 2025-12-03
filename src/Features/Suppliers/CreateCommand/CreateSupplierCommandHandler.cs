using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.CreateCommand;
public sealed class CreateSupplierCommandHandler(ISupplierRepository supplierRepository) : IRequestHandler<CreateSupplierCommand, ValidationResult>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;

    public async Task<ValidationResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.CompanyName, request.Address, request.Email, request.TaxIdType, request.TaxId);

        var result = await _supplierRepository.AddAsync(supplier, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _supplierRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}