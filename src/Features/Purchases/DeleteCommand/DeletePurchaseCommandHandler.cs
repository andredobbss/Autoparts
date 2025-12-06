using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Purchases.DeleteCommand;

public sealed class DeletePurchaseCommandHandler(AutopartsDbContext context, IStockCalculator stockCalculator) : IRequestHandler<DeletePurchaseCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases!.FindAsync(request.PurchaseId, cancellationToken);
        if (purchase is null)
            return new ValidationResult([new ValidationFailure(Resource.PURCHASE, string.Format(Resource.PURCHASE_NOT_FOUND, request.PurchaseId))]);

        purchase.Delete();

        _context.Purchases.Update(purchase);

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