using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.DeleteCommand;

public sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<ValidationResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, string.Format(Resource.PRODUCTS_NOT_FOUND, request.ProductId))]);

        product.Delete();

        var deleted = await _productRepository.DeleteAsync(product, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.PRODUCT, Resource.FAILED_TO_DELETE_PRODUCT)]);

        var committed = await _productRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}