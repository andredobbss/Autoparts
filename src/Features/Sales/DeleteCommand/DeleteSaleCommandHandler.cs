using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Sales.DeleteCommand;

public sealed class DeleteSaleCommandHandler(ISaleRepository saleRepository) : IRequestHandler<DeleteSaleCommand, ValidationResult>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    public async Task<ValidationResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale is null)
            return new ValidationResult([new ValidationFailure(Resource.SALE, string.Format(Resource.SALES_NOT_FOUND, request.SaleId))]);

        sale.Delete();

        var deleted = await _saleRepository.DeleteAsync(sale, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.SALE, Resource.FAILED_TO_DELETE_SALE)]);

        var committed = await _saleRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}


