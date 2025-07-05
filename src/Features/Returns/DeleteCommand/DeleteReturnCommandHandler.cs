using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Stock;
using MediatR;
namespace Autoparts.Api.Features.Returns.DeleteCommand;
public sealed class DeleteReturnCommandHandler(IReturnRepository returnRepository, IStockCalculator stockCalculator) : IRequestHandler<DeleteReturnCommand, bool>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<bool> Handle(DeleteReturnCommand request, CancellationToken cancellationToken)
    {
        var returnItem = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnItem is null)
            return false;

        var products = returnItem.Products
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

        //    returnItem.Delete();

        //    await _returnRepository.DeleteAsync(returnItem, cancellationToken);

        //    return true;
        //}
        //catch
        //{
        //    return false;
        //}
        return true;
    }
}