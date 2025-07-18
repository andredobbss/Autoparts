using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Products.Stock;
using MediatR;
namespace Autoparts.Api.Features.Purchases.DeleteCommand;
public sealed class DeletePurchaseCommandHandler(IPurchaseRepository purchaseRepository, IStockCalculator stockCalculator) : IRequestHandler<DeletePurchaseCommand, bool>
{
    private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<bool> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return false;

        purchase.Delete();

        await _purchaseRepository.DeleteAsync(purchase, cancellationToken);

        await _purchaseRepository.Commit(cancellationToken);

        await _stockCalculator.StockCalculateAsync(cancellationToken);

        return true;
    }
}