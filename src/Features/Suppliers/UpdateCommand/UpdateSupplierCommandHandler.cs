using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.UpdateCommand;
public sealed class UpdateSupplierCommandHandler(ISupplierRepository supplierRepository) :IRequestHandler<UpdateSupplierCommand, ValidationResult >
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<ValidationResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
            return new ValidationResult { Errors = { new ValidationFailure(Resource.SUPPLIER, string.Format(Resource.SUPPLIER_NOT_FOUND, request.SupplierId)) } };
      
        supplier.Update(request.CompanyName, request.Address, request.Email, request.TaxIdType, request.TaxId);

        var result = await _supplierRepository.UpdateAsync(supplier, cancellationToken);
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