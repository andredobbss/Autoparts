using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Suppliers.UpdateCommand;
public sealed class UpdateSupplierCommandHandler(AutopartsDbContext context) :IRequestHandler<UpdateSupplierCommand, ValidationResult >
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _context.Suppliers!.FindAsync(request.SupplierId, cancellationToken);
        if (supplier is null)
            return new ValidationResult { Errors = { new ValidationFailure(Resource.SUPPLIER, string.Format(Resource.SUPPLIER_NOT_FOUND, request.SupplierId)) } };
      
        supplier.Update(request.CompanyName, request.Address, request.Email, request.TaxIdType, request.TaxId);

        _context.Suppliers!.Update(supplier);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
        {
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        return new ValidationResult();
    }
}