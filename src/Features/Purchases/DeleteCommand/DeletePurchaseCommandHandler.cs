using Autoparts.Api.Features.Purchases.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed class DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<DeletePurchaseCommand, bool>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    public async Task<bool> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return false;

        purchase.Delete();

        var deleted = await _purchaseRepository.DeleteAsync(purchase, cancellationToken);
        if (!deleted)
            return false;

        var committed = await _purchaseRepository.CommitAsync(cancellationToken);
        return committed;
    }
}