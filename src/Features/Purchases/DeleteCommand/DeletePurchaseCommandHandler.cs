using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Shared.Enums;
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

        var products = purchase.Products
            .GroupBy(p => p.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalStock = g.Sum(p => p.Stock)
            })
            .ToList();

        if (products.Count == 0)
            return false;

        //try
        //{
        //    foreach (var product in products)
        //        await _stockCalculator.CalculateStockAsync(product.ProductId, (uint)product.TotalStock, ECalculationType.Subtraction, cancellationToken);

        //    purchase.Delete();

        //    await _purchaseRepository.DeleteAsync(purchase, cancellationToken);

        //    return true;
        //}
        //catch
        //{
        //    return false;
        //}
        return true;
    }
}