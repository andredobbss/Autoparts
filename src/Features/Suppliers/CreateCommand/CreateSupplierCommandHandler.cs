using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.CreateCommand;
public sealed class CreateSupplierCommandHandler(ISupplierRepository supplierRepository) : IRequestHandler<CreateSupplierCommand, ValidationResult>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<ValidationResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.CompanyName, request.Address);

        var result = await _supplierRepository.AddAsync(supplier, cancellationToken);

        return result;
    }
}