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
            return new ValidationResult { Errors = { new ValidationFailure("Supplier", $"{Resource.ID_NOT_FOUND} : {request.SupplierId}") } };
      
        supplier.Update(request.CompanyName, request.Address);

        var result = await _supplierRepository.UpdateAsync(supplier, cancellationToken);

        return result;
    }
}