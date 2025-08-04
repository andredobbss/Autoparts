using Autoparts.Api.Features.Returns.Infraestructure;
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

        returnItem.Delete();

        await _returnRepository.DeleteAsync(returnItem, cancellationToken);

        await _returnRepository.Commit(cancellationToken);

        await _stockCalculator.StockCalculateAsync(cancellationToken);

        return true;
    }
}