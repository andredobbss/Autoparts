using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Returns.DeleteCommand;
public sealed class DeleteReturnCommandHandler(AutopartsDbContext context, IStockCalculator stockCalculator) : IRequestHandler<DeleteReturnCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(DeleteReturnCommand request, CancellationToken cancellationToken)
    {
        var returnItem = await context.Returns!.FindAsync(request.ReturnId, cancellationToken);
        if (returnItem is null)
            return new ValidationResult([new ValidationFailure(Resource.RETURN, string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId))]);

        returnItem.Delete();

        context.Returns!.Update(returnItem);

        #region Transaction and Commit

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var commitResult = await context.SaveChangesAsync(cancellationToken);

            bool stockUpdated = await stockCalculator.StockCalculateAsync(cancellationToken);

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