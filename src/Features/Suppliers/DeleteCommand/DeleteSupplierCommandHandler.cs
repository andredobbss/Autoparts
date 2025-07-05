using Autoparts.Api.Features.Suppliers.Infraestructure;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.DeleteCommand;
public sealed class DeleteSupplierCommandHandler(ISupplierRepository supplierRepository) : IRequestHandler<DeleteSupplierCommand, bool>
{
    private readonly ISupplierRepository _supplierRepository = supplierRepository;
    public async Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
            return false;

        supplier.Delete();

        await _supplierRepository.DeleteAsync(supplier, cancellationToken);
        return true;
    }
}