using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Sales.DeleteCommand;

public sealed class DeleteSaleCommandHandler(AutopartsDbContext context, IStockCalculator stockCalculator) : IRequestHandler<DeleteSaleCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales!.FindAsync(request.SaleId, cancellationToken);
        if (sale is null)
            return new ValidationResult([new ValidationFailure(Resource.SALE, string.Format(Resource.SALES_NOT_FOUND, request.SaleId))]);

        sale.Delete();

        _context.Sales!.Update(sale);

        #region Transaction and Commit

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await _context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await _stockCalculator.StockCalculateAsync(cancellationToken);

            if (commitResult > 0 && stockUpdated)
            {
                await transaction.CommitAsync(cancellationToken);
                return new ValidationResult();
            }

            await transaction.RollbackAsync(cancellationToken);
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        #endregion
    }
}


