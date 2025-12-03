using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.DeleteCommand;
public sealed class DeleteSupplierCommandHandler(ISupplierRepository supplierRepository) : IRequestHandler<DeleteSupplierCommand, ValidationResult>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<ValidationResult> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
            return new ValidationResult([new ValidationFailure(Resource.SUPPLIER, string.Format(Resource.SUPPLIER_NOT_FOUND, request.SupplierId))]);

        supplier.Delete();

        var deleted = await _supplierRepository.DeleteAsync(supplier, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.SUPPLIER, Resource.FAILED_TO_DELETE_SUPPLIER)]);

        var committed = await _supplierRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}