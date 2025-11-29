using Autoparts.Api.Features.Sales.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Sales.DeleteCommand;

public sealed class DeleteSaleCommandHandler(ISaleRepository saleRepository) : IRequestHandler<DeleteSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale == null)
            return false;

        sale.Delete();

        var deleted = await _saleRepository.DeleteAsync(sale, cancellationToken);
        if (!deleted)
            return false;

        var committed = await _saleRepository.CommitAsync(cancellationToken);
        return committed;
    }
}


