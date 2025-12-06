using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Suppliers.CreateCommand;
public sealed class CreateSupplierCommandHandler(AutopartsDbContext context) : IRequestHandler<CreateSupplierCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;

    public async Task<ValidationResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.CompanyName, request.Address, request.Email, request.TaxIdType, request.TaxId);

        _context.Suppliers!.Add(supplier);

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