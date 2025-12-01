using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed class DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<DeletePurchaseCommand, ValidationResult>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<ValidationResult> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return new ValidationResult([new ValidationFailure(Resource.PURCHASE, string.Format(Resource.PURCHASE_NOT_FOUND, request.PurchaseId))]);

        purchase.Delete();

        var deleted = await _purchaseRepository.DeleteAsync(purchase, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.PURCHASE, Resource.FAILED_TO_DELETE_PURCHASE)]);
        
        var committed = await _purchaseRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}