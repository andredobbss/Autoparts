using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Products.DTOs;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Returns.UpdateCommand;

public sealed class UpdateReturnCommandHandler(AutopartsDbContext context, IProductList productList, IStockCalculator stockCalculator) : IRequestHandler<UpdateReturnCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    private readonly IProductList _productList = productList;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(UpdateReturnCommand request, CancellationToken cancellationToken)
    {
        if (request.Products is null || !request.Products.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_REQUIRED)]);

        var productsList = await _productList.GetProductsListAsync(request.Products.Select(p => new LineItemDto(p.ProductId, p.Quantity)), cancellationToken);
        if (productsList is null || !productsList.Any())
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.PRODUCTS_NOT_FOUND)]);

        var returnEntity = await _context.Returns!.FindAsync(request.ReturnId, cancellationToken);
        if (returnEntity is null)
            return new ValidationResult([new ValidationFailure(Resource.RETURN, string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId))]);

        var returnProducts = productsList.Select(p => new ReturnProduct(request.ReturnId, p.ProductId, p.Quantity, p.SellingPrice, request.Products.Select(p => p.Loss).FirstOrDefault())).ToList();

        returnEntity.Update(
            request.Justification,
            request.InvoiceNumber,
            request.UserId,
            request.ClientId,
            returnProducts);

        _context.Returns!.Update(returnEntity);

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